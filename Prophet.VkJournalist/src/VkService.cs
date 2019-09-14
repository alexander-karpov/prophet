using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;
using Prophet.VkJournalist.VkProtocol;
using Prophet.VkJournalist.Model;

namespace Prophet.VkJournalist
{
    public class VkService
    {
        readonly HttpClient client = new HttpClient();

        public async Task<IEnumerable<Post>> WallGet(string ownerId, int count, int offset = 0)
        {
            var responseBody = await client.GetStringAsync(
                $"https://api.vk.com/method/wall.get?owner_id={ownerId}&v=5.52&access_token=eff437ddeff437ddeff437dddbef9faed2eeff4eff437ddb2d1ece298491d125b8291d4&count={count}&offset={offset}"
            );

            var wallGetResponse = JsonSerializer.Deserialize<ApiResponse<WallGetResponse>>(
                responseBody,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            if (wallGetResponse.Error != null && wallGetResponse.Response != null)
            {
                Console.Error.WriteLine(wallGetResponse.Error.ErrorMsg);
                return Enumerable.Empty<Post>();
            }

            return wallGetResponse
                .Response
                .Items
                .Select(item => new Post
                {
                    Id = item.Id,
                    OwnerId = item.OwnerId,
                    FromId = item.FromId,
                    Text = item.Text,
                    Date = item.Date
                });
        }
    }
}
