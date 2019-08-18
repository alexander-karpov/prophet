using System.Net.Http;
using Prophet.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace VK.OpenApi
{
    public class VkOpenApiClient
    {
        readonly IHttpClientFactory _clientFactory;
        readonly IJsonSerializer _jsonSerializer;
        readonly ILogger _logger;

        public VkOpenApiClient(
            IHttpClientFactory clientFactory,
            IJsonSerializer jsonSerializer,
            ILogger<VkOpenApiClient> logger
        )
        {
            _clientFactory = clientFactory;
            _jsonSerializer = jsonSerializer;
            _logger = logger;
        }

        public async Task<IEnumerable<(int id, string text)>> WallGet(string ownerId, int count, int offset = 0)
        {
            var responseBody = await _clientFactory.CreateClient().GetStringAsync(
                $"https://api.vk.com/method/wall.get?owner_id={ownerId}&v=5.52&access_token=eff437ddeff437ddeff437dddbef9faed2eeff4eff437ddb2d1ece298491d125b8291d4&count={count}&offset={offset}"
            );

            var wallGetResponse = _jsonSerializer.Deserialize<ApiResponse<WallGetResponse>>(responseBody);

            if (wallGetResponse.Error != null)
            {
                _logger.LogWarning(wallGetResponse.Error.ErrorMsg, ownerId);
                return Enumerable.Empty<(int, string)>();
            }

            return wallGetResponse
                .Response
                .Items
                .OrderBy(i => i.Date)
                .Select(item => (item.Id, item.Text))
                .ToArray();
        }
    }
}
