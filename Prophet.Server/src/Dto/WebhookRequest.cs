namespace Prophet.Dto
{
    public class WebhookRequest
    {
        public string Action { get; set; }
        public string QueryText { get; set; }
        public string UserId { get; set; }
    }
}
