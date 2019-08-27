using System.Text.Json;

namespace Prophet.Text.Json
{
    public class SnakeCaseJsonSerializer : IJsonSerializer
    {
        private JsonSerializerOptions _jsonOptions;

        public SnakeCaseJsonSerializer()
        {
            _jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy() };
        }

        public TValue Deserialize<TValue>(string json)
        {
            return JsonSerializer.Deserialize<TValue>(json, _jsonOptions);
        }

        public string Serialize<TValue>(TValue value)
        {
            return JsonSerializer.Serialize(value, _jsonOptions);
        }
    }
}
