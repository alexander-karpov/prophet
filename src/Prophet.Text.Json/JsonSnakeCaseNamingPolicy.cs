using System.Collections.Generic;
using System.Text.Json;

namespace Prophet.Text.Json
{
    public sealed class JsonSnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            if (string.IsNullOrEmpty(name) || !char.IsUpper(name[0]))
            {
                return name;
            }

            var chars = name.ToCharArray();

            var underscoresReserve = 6;
            var result = new List<char>(chars.Length + underscoresReserve);

            for (var i = 0; i < chars.Length; i++)
            {
                if (i != 0 && char.IsUpper(chars[i]))
                {
                    result.Add('_');
                }

                result.Add(char.ToLowerInvariant(chars[i]));
            }

            return new string(result.ToArray());
        }
    }
}
