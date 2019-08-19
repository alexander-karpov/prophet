namespace Prophet.Entities
{
    public class Article
    {
        public int ArticleId { get; set; }
        public int ArticleOwnId { get; set; }
        public string Text { get; set; }

        public int FeedId { get; set; }
        public Feed Feed { get; set; }
    }
}
