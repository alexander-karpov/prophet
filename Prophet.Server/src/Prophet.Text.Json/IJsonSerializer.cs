namespace Prophet.Text.Json
{
    public interface IJsonSerializer
    {
        TValue Deserialize<TValue>(string json);
        string Serialize<TValue>(TValue value);
    }
}
