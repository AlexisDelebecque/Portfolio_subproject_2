using System.Collections.Generic;
using System.Linq;
using WebApi.Domain.UserDomain;

namespace WebApi.Services.UserServices
{
    public class NameBookmarkService
    {
        private readonly PortfolioContext _ctx;

        public NameBookmarkService()
        {
            _ctx = new PortfolioContext();
        }
        
        public IList<NameBookmark> GetNameBookmarks(string username)
        {
            return _ctx.NameBookmarks
                .Where(x => x.Username == username)
                .ToList();
        }

        public NameBookmark GetNameBookmark(string username, string nameId)
        {
            return _ctx.NameBookmarks
                .FirstOrDefault(x => x.Username == username && x.NameId == nameId);
        }
        
        public NameBookmark CreateNameBookmark(string username, string nameId)
        {
            if (GetNameBookmark(username, nameId) != null)
                return null;
            
            var nameBookmark = new NameBookmark
            {
                Username = username,
                NameId = nameId,
            };

            _ctx.NameBookmarks.Add(nameBookmark);
            _ctx.SaveChanges();
            return nameBookmark;
        }
        
        public bool DeleteNameBookmark(string username, string nameId)
        {
            var nameBookmarkToRemove = GetNameBookmark(username, nameId);

            if (nameBookmarkToRemove == null)
                return false;
            
            _ctx.NameBookmarks.Remove(nameBookmarkToRemove);
            return _ctx.SaveChanges() > 0;
        }
    }
}