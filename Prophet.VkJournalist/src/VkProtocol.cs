using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Prophet.VkJournalist.VkProtocol
{
    public class ApiResponse<TResponse>
    {
        public TResponse Response { get; set; }
        public ApiError Error { get; set; }

        public void Deconstruct(out TResponse response)
        {
            response = Response;
        }
    }

    public class WallGetResponse
    {
        public IEnumerable<WallItem> Items { get; set; }
    }

    public class WallItem
    {
        public int Id { get; set; }

        [JsonPropertyName("owner_id")]
        public int OwnerId { get; set; }

        [JsonPropertyName("from_id")]
        public int FromId { get; set; }

        public string Text { get; set; }
        public int Date { get; set; }
    }

    public class ApiError
    {
        [JsonPropertyName("error_code")]
        public int ErrorCode { get; set; }

        [JsonPropertyName("error_msg")]
        public string ErrorMsg { get; set; }
    }
}
