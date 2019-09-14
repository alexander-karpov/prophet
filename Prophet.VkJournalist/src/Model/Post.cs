namespace Prophet.VkJournalist.Model
{
    public class Post
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int FromId { get; set; }
        public string Text { get; set; }
        public int Date { get; set; }
    }
}
