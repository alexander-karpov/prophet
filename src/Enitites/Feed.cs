using System.Collections.Generic;
using System;

namespace Prophet.Entities
{
    public class Feed
    {
        public int FeedId { get; set; }
        public string Platform { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime LastPulledOn { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
