using System;
using System.Linq;
using WebApi.Domain.UserDomain;
using WebApi.Services.UserServices;
using Xunit;

namespace WebApiTests
{
    public class NameBookmarkServiceTest
    {
        [Fact]
        public void NameBookmark_Object_HasDefaultValues()
        {
            var rating = new NameBookmark();
            Assert.Null(rating.Username);
            Assert.Null(rating.NameId);
        }

        [Fact]
        public void CreateNameBookmark_ValidData_CreteNameBookmarkAndReturnsNewObject()
        {
            UserUtils.InitUser();
            var service = new NameBookmarkService();
            var rating = service.CreateNameBookmark("test", "nm9041227");
            Assert.Equal("test", rating.Username);
            Assert.Equal("nm9041227", rating.NameId);

            // cleanup
            UserUtils.DeleteUser();
            service.DeleteNameBookmark(rating.Username, rating.NameId);
        }
        
        [Fact]
        public void CreateNameBookmark_ValidDataButAlreadyExisting_ReturnsNullObject()
        {
            UserUtils.InitUser();
            var service = new NameBookmarkService();
            var nameBookmark = service.CreateNameBookmark("test", "nm9041227");
            var sameNameBookmark = service.CreateNameBookmark("test", "nm9041227");
            Assert.Null(sameNameBookmark);

            // cleanup
            UserUtils.DeleteUser();
            service.DeleteNameBookmark(nameBookmark.Username, nameBookmark.NameId);
        }
        
        [Fact]
        public void GetAllSearchHistories_ValidUsernameAndBasicPage_ReturnsFirstPage()
        {
            UserUtils.InitUser();
            var service = new NameBookmarkService();
            var nameBookmark1 = service.CreateNameBookmark("test", "nm9041227");
            var nameBookmark2 = service.CreateNameBookmark("test", "nm7172762");
            var nameBookmark3 = service.CreateNameBookmark("test", "nm0933988");
            var nameBookmark4 = service.CreateNameBookmark("test", "nm4663392");
            var nameBookmark5 = service.CreateNameBookmark("test", "nm0202516");
            var nameBookmarks = service.GetNameBookmarks("test", 0, 10);
            Assert.Equal(5, nameBookmarks.Count);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteNameBookmark(nameBookmark1.Username, nameBookmark1.NameId);
            service.DeleteNameBookmark(nameBookmark2.Username, nameBookmark2.NameId);
            service.DeleteNameBookmark(nameBookmark3.Username, nameBookmark3.NameId);
            service.DeleteNameBookmark(nameBookmark4.Username, nameBookmark4.NameId);
            service.DeleteNameBookmark(nameBookmark5.Username, nameBookmark5.NameId);
        }
        
        [Fact]
        public void GetAllSearchHistories_ValidUsernameAndOutsidePage_ReturnsEmptyList()
        {
            UserUtils.InitUser();
            var service = new NameBookmarkService();
            var nameBookmark1 = service.CreateNameBookmark("test", "nm9041227");
            var nameBookmark2 = service.CreateNameBookmark("test", "nm7172762");
            var nameBookmark3 = service.CreateNameBookmark("test", "nm0933988");
            var nameBookmark4 = service.CreateNameBookmark("test", "nm4663392");
            var nameBookmark5 = service.CreateNameBookmark("test", "nm0202516");
            var nameBookmarks = service.GetNameBookmarks("test", 1, 10);
            Assert.Equal(0, nameBookmarks.Count);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteNameBookmark(nameBookmark1.Username, nameBookmark1.NameId);
            service.DeleteNameBookmark(nameBookmark2.Username, nameBookmark2.NameId);
            service.DeleteNameBookmark(nameBookmark3.Username, nameBookmark3.NameId);
            service.DeleteNameBookmark(nameBookmark4.Username, nameBookmark4.NameId);
            service.DeleteNameBookmark(nameBookmark5.Username, nameBookmark5.NameId);
        }
        
        [Fact]
        public void GetAllSearchHistories_InValidUsername_ReturnsEmptyList()
        {
            UserUtils.InitUser();
            var service = new NameBookmarkService();
            var nameBookmark1 = service.CreateNameBookmark("test", "nm9041227");
            var nameBookmark2 = service.CreateNameBookmark("test", "nm7172762");
            var nameBookmark3 = service.CreateNameBookmark("test", "nm0933988");
            var nameBookmark4 = service.CreateNameBookmark("test", "nm4663392");
            var nameBookmark5 = service.CreateNameBookmark("test", "nm0202516");
            var nameBookmarks = service.GetNameBookmarks("test2", 0, 10);
            Assert.Equal(0, nameBookmarks.Count);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteNameBookmark(nameBookmark1.Username, nameBookmark1.NameId);
            service.DeleteNameBookmark(nameBookmark2.Username, nameBookmark2.NameId);
            service.DeleteNameBookmark(nameBookmark3.Username, nameBookmark3.NameId);
            service.DeleteNameBookmark(nameBookmark4.Username, nameBookmark4.NameId);
            service.DeleteNameBookmark(nameBookmark5.Username, nameBookmark5.NameId);
        }
        
        [Fact]
        public void GetNameBookmark_ValidUsernameAndNameId_ReturnsNameBookmarkObject()
        {
            UserUtils.InitUser();
            var service = new NameBookmarkService();
            var createNameBookmark = service.CreateNameBookmark("test", "nm9041227");
            Assert.NotNull(createNameBookmark);
            var nameBookmark = service.GetNameBookmark("test", "nm9041227");
            Assert.Equal("test", nameBookmark.Username);
            Assert.Equal("nm9041227", nameBookmark.NameId);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteNameBookmark(nameBookmark.Username, nameBookmark.NameId);
        }
        
        [Fact]
        public void GetNameBookmark_InvalidUsername_ReturnsNullObject()
        {
            UserUtils.InitUser();
            var service = new NameBookmarkService();
            var createNameBookmark = service.CreateNameBookmark("test", "nm9041227");
            Assert.NotNull(createNameBookmark);
            var nameBookmark = service.GetNameBookmark("notExist", "nm9041227");
            Assert.Null(nameBookmark);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteNameBookmark(createNameBookmark.Username, createNameBookmark.NameId);
        }
        
        [Fact]
        public void GetNameBookmark_InvalidNameId_ReturnsNullObject()
        {
            UserUtils.InitUser();
            var service = new NameBookmarkService();
            var createNameBookmark = service.CreateNameBookmark("test", "nm9041227");
            Assert.NotNull(createNameBookmark);
            var nameBookmark = service.GetNameBookmark("test", "notExist");
            Assert.Null(nameBookmark);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteNameBookmark(createNameBookmark.Username, createNameBookmark.NameId);
        }

        [Fact]
        public void DeleteNameBookmark_ValidUsernameAndNameId_RemoveTheNameBookmark()
        {
            UserUtils.InitUser();
            var service = new NameBookmarkService();
            var rating = service.CreateNameBookmark("test", "nm9041227");
            var result = service.DeleteNameBookmark(rating.Username, rating.NameId);
            Assert.True(result);
            rating = service.GetNameBookmark(rating.Username, rating.NameId);
            Assert.Null(rating);
            
            // cleanup
            UserUtils.DeleteUser();
        }

        [Fact]
        public void DeleteNameBookmark_InvalidUsername_ReturnsFalse()
        {
            UserUtils.InitUser();
            var service = new NameBookmarkService();
            var nameBookmark = service.CreateNameBookmark("test", "nm9041227");
            var result = service.DeleteNameBookmark("notExist", "nm9041227");
            Assert.False(result);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteNameBookmark(nameBookmark.Username, nameBookmark.NameId);
        }
        
        [Fact]
        public void DeleteNameBookmark_InvalidNameId_ReturnsFalse()
        {
            UserUtils.InitUser();
            var service = new NameBookmarkService();
            var nameBookmark = service.CreateNameBookmark("test", "nm9041227");
            var result = service.DeleteNameBookmark("test", "notExist");
            Assert.False(result);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteNameBookmark(nameBookmark.Username, nameBookmark.NameId);
        }
    }
}