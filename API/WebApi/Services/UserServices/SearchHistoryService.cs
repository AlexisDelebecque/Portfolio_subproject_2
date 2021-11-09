using System.Collections.Generic;
using System.Linq;
using WebApi.Domain.UserDomain;

namespace WebApi.Services.UserServices
{
    public class SearchHistoryService
    {
        private readonly PortfolioContext _ctx;

        public SearchHistoryService()
        {
            _ctx = new PortfolioContext();
        }
        
        public IList<SearchHistory> GetSearchHistories(string username)
        {
            return _ctx.SearchHistories
                .Where(x => x.Username == username)
                .ToList();
        }
        
        public SearchHistory GetSearchHistory(string username, string searchKey)
        {
            return _ctx.SearchHistories
                .FirstOrDefault(x => x.Username == username && x.SearchKey == searchKey);
        }
        
        public SearchHistory CreateSearchHistory(string username, string searchKey)
        {
            if (GetSearchHistory(username, searchKey) != null)
                return null;
            
            var searchHistory = new SearchHistory
            {
                Username = username,
                SearchKey = searchKey,
            };

            _ctx.SearchHistories.Add(searchHistory);
            _ctx.SaveChanges();
            return searchHistory;
        }
        
        public bool DeleteSearchHistory(string username, string searchKey)
        {
            var searchHistoryToRemove = GetSearchHistory(username, searchKey);

            if (searchHistoryToRemove == null)
                return false;
            
            _ctx.SearchHistories.Remove(searchHistoryToRemove);
            return _ctx.SaveChanges() > 0;
        }
    }
}