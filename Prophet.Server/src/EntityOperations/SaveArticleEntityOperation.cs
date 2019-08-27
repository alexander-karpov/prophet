using Prophet.Entities;

namespace Prophet.EntityOperations
{
    public class SaveArticleEntityOperation
    {
        readonly ProphetContext _db;

        public SaveArticleEntityOperation(ProphetContext db)
        {
            _db = db;
        }

        public Article SaveArticle((int id, string text) data, Feed feed)
        {
            var article = new Article
            {
                ArticleOwnId = data.id,
                Text = data.text,
                FeedId = feed.FeedId,
                Feed = feed
            };

            _db.Articles.Add(article);
            _db.SaveChanges();

            return article;
        }

    }
}
