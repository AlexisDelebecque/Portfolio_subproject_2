using System;
using System.Linq;
using WebApi.Domain.UserDomain;
using WebApi.Services.UserServices;
using Xunit;

namespace WebApiTests
{
    public class SearchHistoryServiceTest
    {
        [Fact]
        public void SearchHistory_Object_HasDefaultValues()
        {
            var rating = new SearchHistory();
            Assert.Null(rating.Username);
            Assert.Null(rating.SearchKey);
        }

        [Fact]
        public void CreateSearchHistory_ValidData_CreteSearchHistoryAndReturnsNewObject()
        {
            UserUtils.InitUser();
            var service = new SearchHistoryService();
            var rating = service.CreateSearchHistory("test", "money");
            Assert.Equal("test", rating.Username);
            Assert.Equal("money", rating.SearchKey);

            // cleanup
            UserUtils.DeleteUser();
            service.DeleteSearchHistory(rating.Username, rating.SearchKey);
        }
        
        [Fact]
        public void CreateSearchHistory_ValidDataButAlreadyExisting_ReturnsNullObject()
        {
            UserUtils.InitUser();
            var service = new SearchHistoryService();
            var searchHistory = service.CreateSearchHistory("test", "money");
            var sameSearchHistory = service.CreateSearchHistory("test", "money");
            Assert.Null(sameSearchHistory);

            // cleanup
            UserUtils.DeleteUser();
            service.DeleteSearchHistory(searchHistory.Username, searchHistory.SearchKey);
        }
        
        [Fact]
        public void GetAllSearchHistories_ValidUsernameAndBasicPage_ReturnsFirstPage()
        {
            UserUtils.InitUser();
            var service = new SearchHistoryService();
            var searchHistory1 = service.CreateSearchHistory("test", "money");
            var searchHistory2 = service.CreateSearchHistory("test", "honey");
            var searchHistory3 = service.CreateSearchHistory("test", "bendy");
            var searchHistory4 = service.CreateSearchHistory("test", "johnny");
            var searchHistory5 = service.CreateSearchHistory("test", "freddy");
            var searchHistories = service.GetSearchHistories("test", 0, 10);
            Assert.Equal(5, searchHistories.Count);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteSearchHistory(searchHistory1.Username, searchHistory1.SearchKey);
            service.DeleteSearchHistory(searchHistory2.Username, searchHistory2.SearchKey);
            service.DeleteSearchHistory(searchHistory3.Username, searchHistory3.SearchKey);
            service.DeleteSearchHistory(searchHistory4.Username, searchHistory4.SearchKey);
            service.DeleteSearchHistory(searchHistory5.Username, searchHistory5.SearchKey);
        }
        
        [Fact]
        public void GetAllSearchHistories_ValidUsernameAndOutsidePage_ReturnsEmptyList()
        {
            UserUtils.InitUser();
            var service = new SearchHistoryService();
            var searchHistory1 = service.CreateSearchHistory("test", "money");
            var searchHistory2 = service.CreateSearchHistory("test", "honey");
            var searchHistory3 = service.CreateSearchHistory("test", "bendy");
            var searchHistory4 = service.CreateSearchHistory("test", "johnny");
            var searchHistory5 = service.CreateSearchHistory("test", "freddy");
            var searchHistories = service.GetSearchHistories("test", 1, 10);
            Assert.Equal(0, searchHistories.Count);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteSearchHistory(searchHistory1.Username, searchHistory1.SearchKey);
            service.DeleteSearchHistory(searchHistory2.Username, searchHistory2.SearchKey);
            service.DeleteSearchHistory(searchHistory3.Username, searchHistory3.SearchKey);
            service.DeleteSearchHistory(searchHistory4.Username, searchHistory4.SearchKey);
            service.DeleteSearchHistory(searchHistory5.Username, searchHistory5.SearchKey);
        }
        
        [Fact]
        public void GetAllSearchHistories_InValidUsername_ReturnsEmptyList()
        {
            UserUtils.InitUser();
            var service = new SearchHistoryService();
            var searchHistory1 = service.CreateSearchHistory("test", "money");
            var searchHistory2 = service.CreateSearchHistory("test", "honey");
            var searchHistory3 = service.CreateSearchHistory("test", "bendy");
            var searchHistory4 = service.CreateSearchHistory("test", "johnny");
            var searchHistory5 = service.CreateSearchHistory("test", "freddy");
            var searchHistories = service.GetSearchHistories("test2", 0, 10);
            Assert.Equal(0, searchHistories.Count);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteSearchHistory(searchHistory1.Username, searchHistory1.SearchKey);
            service.DeleteSearchHistory(searchHistory2.Username, searchHistory2.SearchKey);
            service.DeleteSearchHistory(searchHistory3.Username, searchHistory3.SearchKey);
            service.DeleteSearchHistory(searchHistory4.Username, searchHistory4.SearchKey);
            service.DeleteSearchHistory(searchHistory5.Username, searchHistory5.SearchKey);
        }
        
        [Fact]
        public void GetSearchHistory_ValidUsernameAndSearchKey_ReturnsSearchHistoryObject()
        {
            UserUtils.InitUser();
            var service = new SearchHistoryService();
            var createSearchHistory = service.CreateSearchHistory("test", "money");
            Assert.NotNull(createSearchHistory);
            var searchHistory = service.GetSearchHistory("test", "money");
            Assert.Equal("test", searchHistory.Username);
            Assert.Equal("money", searchHistory.SearchKey);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteSearchHistory(searchHistory.Username, searchHistory.SearchKey);
        }
        
        [Fact]
        public void GetSearchHistory_InvalidUsername_ReturnsNullObject()
        {
            UserUtils.InitUser();
            var service = new SearchHistoryService();
            var createSearchHistory = service.CreateSearchHistory("test", "money");
            Assert.NotNull(createSearchHistory);
            var searchHistory = service.GetSearchHistory("notExist", "money");
            Assert.Null(searchHistory);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteSearchHistory(createSearchHistory.Username, createSearchHistory.SearchKey);
        }
        
        [Fact]
        public void GetSearchHistory_InvalidSearchKey_ReturnsNullObject()
        {
            UserUtils.InitUser();
            var service = new SearchHistoryService();
            var createSearchHistory = service.CreateSearchHistory("test", "money");
            Assert.NotNull(createSearchHistory);
            var searchHistory = service.GetSearchHistory("test", "notExist");
            Assert.Null(searchHistory);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteSearchHistory(createSearchHistory.Username, createSearchHistory.SearchKey);
        }

        [Fact]
        public void DeleteSearchHistory_ValidUsernameAndSearchKey_RemoveTheSearchHistory()
        {
            UserUtils.InitUser();
            var service = new SearchHistoryService();
            var searchHistory = service.CreateSearchHistory("test", "money");
            var result = service.DeleteSearchHistory(searchHistory.Username, searchHistory.SearchKey);
            Assert.True(result);
            searchHistory = service.GetSearchHistory(searchHistory.Username, searchHistory.SearchKey);
            Assert.Null(searchHistory);
            
            // cleanup
            UserUtils.DeleteUser();
        }

        [Fact]
        public void DeleteSearchHistory_InvalidUsername_ReturnsFalse()
        {
            UserUtils.InitUser();
            var service = new SearchHistoryService();
            var searchHistory = service.CreateSearchHistory("test", "money");
            var result = service.DeleteSearchHistory("notExist", "money");
            Assert.False(result);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteSearchHistory(searchHistory.Username, searchHistory.SearchKey);
        }
        
        [Fact]
        public void DeleteSearchHistory_InvalidSearchKey_ReturnsFalse()
        {
            UserUtils.InitUser();
            var service = new SearchHistoryService();
            var searchHistory = service.CreateSearchHistory("test", "money");
            var result = service.DeleteSearchHistory("test", "notExist");
            Assert.False(result);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteSearchHistory(searchHistory.Username, searchHistory.SearchKey);
        }
    }
}