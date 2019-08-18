#nullable enable

using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Prophet.Entities;
using static Prophet.Prelude;

namespace Prophet
{
    public class ReadModel
    {
        readonly ProphetContext _db;

        public ReadModel(ProphetContext storage)
        {
            _db = storage;
        }

        public Maybe<User> UserById(string userId)
        {
            return Maybe(_db.Users.SingleOrDefault(u => u.UserId == userId));
        }

        public Maybe<Article> FreshArticle(User userId)
        {
            var freshArticles = from userFeed in _db.UserFeeds
                                join feed in _db.Feeds on userFeed.FeedId equals feed.FeedId
                                join article in _db.Articles on feed.FeedId equals article.FeedId
                                where userFeed.UserId == userId.UserId && article.ArticleId > userId.LastReceivedArticleId
                                select article;

            return Maybe(freshArticles.FirstOrDefault());
        }

        public int LastArticleId()
        {
            var lastNews = _db.Articles.OrderBy(n => n.ArticleId).LastOrDefault();

            return lastNews?.ArticleId ?? 0;
        }

        public IEnumerable<Feed> Feeds()
        {
            return _db.Feeds.ToArray();
        }

        public Article? FindArticle(int articleOwnId, Feed feed)
        {
            return _db.Articles.FirstOrDefault(
                n => n.ArticleOwnId == articleOwnId && n.FeedId == feed.FeedId
            );
        }

        public bool IsArticleExists(int articleOwnId, Feed feed)
        {
            return _db.Articles.Where(
                n => n.ArticleOwnId == articleOwnId && n.FeedId == feed.FeedId
            ).Count() == 1;
        }
    }
}
