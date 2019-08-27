#nullable disable

using System.Collections.Generic;
using System;

namespace VK.OpenApi
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
        public string Text { get; set; }
        public int Date { get; set; }
    }

    public class ApiError
    {
        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
    }
}
