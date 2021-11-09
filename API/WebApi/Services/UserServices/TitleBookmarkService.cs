using System.Collections.Generic;
using System.Linq;
using WebApi.Domain.UserDomain;

namespace WebApi.Services.UserServices
{
    public class TitleBookmarkService
    {
        private readonly PortfolioContext _ctx;

        public TitleBookmarkService()
        {
            _ctx = new PortfolioContext();
        }
        
        public IList<TitleBookmark> GetTitleBookmarks(string username)
        {
            return _ctx.TitleBookmarks
                .Where(x => x.Username == username)
                .ToList();
        }

        public TitleBookmark GetTitleBookmark(string username, string titleId)
        {
            return _ctx.TitleBookmarks
                .FirstOrDefault(x => x.Username == username && x.TitleId == titleId);
        }
        
        public TitleBookmark CreateTitleBookmark(string username, string titleId)
        {
            if (GetTitleBookmark(username, titleId) != null)
                return null;
            
            var titleBookmark = new TitleBookmark
            {
                Username = username,
                TitleId = titleId,
            };

            _ctx.TitleBookmarks.Add(titleBookmark);
            _ctx.SaveChanges();
            return titleBookmark;
        }
        
        public bool DeleteTitleBookmark(string username, string titleId)
        {
            var titleBookmarkToRemove = GetTitleBookmark(username, titleId);

            if (titleBookmarkToRemove == null)
                return false;
            
            _ctx.TitleBookmarks.Remove(titleBookmarkToRemove);
            return _ctx.SaveChanges() > 0;
        }
    }
}