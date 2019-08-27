namespace Prophet.Dto
{
    public class StartResponse : WebhookResponse
    {
        public bool IsNew { get; set; }
        public string ArticleAuthor { get; set; }
        public string ArticleText { get; set; }
    }
}
