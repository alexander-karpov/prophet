#nullable disable

/* https://yandex.ru/dev/dialogs/alice/doc/protocol-docpage/ */
using System.Text.Json.Serialization;

namespace Prophet.Dialog
{
    public class DialogsRequest
    {
        public WebhookRequestData Request { get; set; }
        public WebhookRequestSession Session { get; set; }
        public string Version { get; set; }
    }

    public class WebhookRequestData
    {
        public string Command { get; set; }
    }

    public class WebhookRequestSession
    {
        [JsonPropertyName("new")]
        public bool IsNew { get; set; }

        [JsonPropertyName("message_id")]
        public uint MessageId { get; set; }

        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
    }

    public class DialogsResponse
    {
        public WebhookResponseData Response { get; set; }
        public WebhookResponseSession Session { get; set; }
        public string Version { get; set; }
    }

    public class WebhookResponseData
    {
        public string Text { get; set; }
        public string Tts { get; set; }
    }

    public class WebhookResponseSession
    {
        [JsonPropertyName("message_id")]
        public uint MessageId { get; set; }

        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
    }
}
