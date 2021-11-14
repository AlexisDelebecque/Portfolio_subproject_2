using System;
using System.Linq;
using WebApi.Domain.UserDomain;
using WebApi.Services.UserServices;
using Xunit;

namespace WebApiTests
{
    public class TitleBookmarkServiceTest
    {
        [Fact]
        public void TitleBookmark_Object_HasDefaultValues()
        {
            var rating = new TitleBookmark();
            Assert.Null(rating.Username);
            Assert.Null(rating.TitleId);
        }

        [Fact]
        public void CreateTitleBookmark_ValidData_CreateTitleBookmarkAndReturnsNewObject()
        {
            UserUtils.InitUser();
            var service = new TitleBookmarkService();
            var rating = service.CreateTitleBookmark("test", "tt10111746");
            Assert.Equal("test", rating.Username);
            Assert.Equal("tt10111746", rating.TitleId);

            // cleanup
            UserUtils.DeleteUser();
            service.DeleteTitleBookmark(rating.Username, rating.TitleId);
        }
        
        [Fact]
        public void CreateTitleBookmark_ValidDataButAlreadyExisting_ReturnsNullObject()
        {
            UserUtils.InitUser();
            var service = new TitleBookmarkService();
            var titleBookmark = service.CreateTitleBookmark("test", "tt10111746");
            var sameTitleBookmark = service.CreateTitleBookmark("test", "tt10111746");
            Assert.Null(sameTitleBookmark);

            // cleanup
            UserUtils.DeleteUser();
            service.DeleteTitleBookmark(titleBookmark.Username, titleBookmark.TitleId);
        }
        
        [Fact]
        public void GetAllSearchHistories_ValidUsernameAndBasicPage_ReturnsFirstPage()
        {
            UserUtils.InitUser();
            var service = new TitleBookmarkService();
            var titleBookmark1 = service.CreateTitleBookmark("test", "tt10111746");
            var titleBookmark2 = service.CreateTitleBookmark("test", "tt12490740");
            var titleBookmark3 = service.CreateTitleBookmark("test", "tt1310664");
            var titleBookmark4 = service.CreateTitleBookmark("test", "tt1220221");
            var titleBookmark5 = service.CreateTitleBookmark("test", "tt0454854");
            var titleBookmarks = service.GetTitleBookmarks("test", 0, 10);
            Assert.Equal(5, titleBookmarks.Count);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteTitleBookmark(titleBookmark1.Username, titleBookmark1.TitleId);
            service.DeleteTitleBookmark(titleBookmark2.Username, titleBookmark2.TitleId);
            service.DeleteTitleBookmark(titleBookmark3.Username, titleBookmark3.TitleId);
            service.DeleteTitleBookmark(titleBookmark4.Username, titleBookmark4.TitleId);
            service.DeleteTitleBookmark(titleBookmark5.Username, titleBookmark5.TitleId);
        }
        
        [Fact]
        public void GetAllSearchHistories_ValidUsernameAndOutsidePage_ReturnsEmptyList()
        {
            UserUtils.InitUser();
            var service = new TitleBookmarkService();
            var titleBookmark1 = service.CreateTitleBookmark("test", "tt10111746");
            var titleBookmark2 = service.CreateTitleBookmark("test", "tt12490740");
            var titleBookmark3 = service.CreateTitleBookmark("test", "tt1310664");
            var titleBookmark4 = service.CreateTitleBookmark("test", "tt1220221");
            var titleBookmark5 = service.CreateTitleBookmark("test", "tt0454854");
            var titleBookmarks = service.GetTitleBookmarks("test", 1, 10);
            Assert.Equal(0, titleBookmarks.Count);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteTitleBookmark(titleBookmark1.Username, titleBookmark1.TitleId);
            service.DeleteTitleBookmark(titleBookmark2.Username, titleBookmark2.TitleId);
            service.DeleteTitleBookmark(titleBookmark3.Username, titleBookmark3.TitleId);
            service.DeleteTitleBookmark(titleBookmark4.Username, titleBookmark4.TitleId);
            service.DeleteTitleBookmark(titleBookmark5.Username, titleBookmark5.TitleId);
        }
        
        [Fact]
        public void GetAllSearchHistories_InValidUsername_ReturnsEmptyList()
        {
            UserUtils.InitUser();
            var service = new TitleBookmarkService();
            var titleBookmark1 = service.CreateTitleBookmark("test", "tt10111746");
            var titleBookmark2 = service.CreateTitleBookmark("test", "tt12490740");
            var titleBookmark3 = service.CreateTitleBookmark("test", "tt1310664");
            var titleBookmark4 = service.CreateTitleBookmark("test", "tt1220221");
            var titleBookmark5 = service.CreateTitleBookmark("test", "tt0454854");
            var titleBookmarks = service.GetTitleBookmarks("test2", 0, 10);
            Assert.Equal(0, titleBookmarks.Count);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteTitleBookmark(titleBookmark1.Username, titleBookmark1.TitleId);
            service.DeleteTitleBookmark(titleBookmark2.Username, titleBookmark2.TitleId);
            service.DeleteTitleBookmark(titleBookmark3.Username, titleBookmark3.TitleId);
            service.DeleteTitleBookmark(titleBookmark4.Username, titleBookmark4.TitleId);
            service.DeleteTitleBookmark(titleBookmark5.Username, titleBookmark5.TitleId);
        }
        
        [Fact]
        public void GetTitleBookmark_ValidUsernameAndTitleId_ReturnsTitleBookmarkObject()
        {
            UserUtils.InitUser();
            var service = new TitleBookmarkService();
            var createTitleBookmark = service.CreateTitleBookmark("test", "tt10111746");
            Assert.NotNull(createTitleBookmark);
            var titleBookmark = service.GetTitleBookmark("test", "tt10111746");
            Assert.Equal("test", titleBookmark.Username);
            Assert.Equal("tt10111746", titleBookmark.TitleId);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteTitleBookmark(titleBookmark.Username, titleBookmark.TitleId);
        }
        
        [Fact]
        public void GetTitleBookmark_InvalidUsername_ReturnsNullObject()
        {
            UserUtils.InitUser();
            var service = new TitleBookmarkService();
            var createTitleBookmark = service.CreateTitleBookmark("test", "tt10111746");
            Assert.NotNull(createTitleBookmark);
            var titleBookmark = service.GetTitleBookmark("notExist", "tt10111746");
            Assert.Null(titleBookmark);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteTitleBookmark(createTitleBookmark.Username, createTitleBookmark.TitleId);
        }
        
        [Fact]
        public void GetTitleBookmark_InvalidTitleId_ReturnsNullObject()
        {
            UserUtils.InitUser();
            var service = new TitleBookmarkService();
            var createTitleBookmark = service.CreateTitleBookmark("test", "tt10111746");
            Assert.NotNull(createTitleBookmark);
            var titleBookmark = service.GetTitleBookmark("test", "notExist");
            Assert.Null(titleBookmark);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteTitleBookmark(createTitleBookmark.Username, createTitleBookmark.TitleId);
        }

        [Fact]
        public void DeleteTitleBookmark_ValidUsernameAndTitleId_RemoveTheTitleBookmark()
        {
            UserUtils.InitUser();
            var service = new TitleBookmarkService();
            var rating = service.CreateTitleBookmark("test", "tt10111746");
            var result = service.DeleteTitleBookmark(rating.Username, rating.TitleId);
            Assert.True(result);
            rating = service.GetTitleBookmark(rating.Username, rating.TitleId);
            Assert.Null(rating);
            
            // cleanup
            UserUtils.DeleteUser();
        }

        [Fact]
        public void DeleteTitleBookmark_InvalidUsername_ReturnsFalse()
        {
            UserUtils.InitUser();
            var service = new TitleBookmarkService();
            var titleBookmark = service.CreateTitleBookmark("test", "tt10111746");
            var result = service.DeleteTitleBookmark("notExist", "tt10111746");
            Assert.False(result);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteTitleBookmark(titleBookmark.Username, titleBookmark.TitleId);
        }
        
        [Fact]
        public void DeleteTitleBookmark_InvalidTitleId_ReturnsFalse()
        {
            UserUtils.InitUser();
            var service = new TitleBookmarkService();
            var titleBookmark = service.CreateTitleBookmark("test", "tt10111746");
            var result = service.DeleteTitleBookmark("test", "notExist");
            Assert.False(result);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteTitleBookmark(titleBookmark.Username, titleBookmark.TitleId);
        }
    }
}