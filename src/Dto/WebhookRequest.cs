namespace Prophet.Dto
{
    public class WebhookRequest
    {
        public string Action { get; set; }
        public string QueryText { get; set; }
        public string UserId { get; set; }
        public string ChannelId { get; set; }
        public string ChannelType { get; set; }
    }
}
