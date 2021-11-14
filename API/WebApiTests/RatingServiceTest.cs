using System;
using System.Linq;
using WebApi.Domain.UserDomain;
using WebApi.Services.UserServices;
using Xunit;

namespace WebApiTests
{
    public class RatingServiceTest
    {
        [Fact]
        public void Rating_Object_HasDefaultValues()
        {
            var rating = new Rating();
            Assert.Null(rating.Username);
            Assert.Null(rating.TitleId);
            Assert.Equal(0, rating.Rate);
            Assert.Null(rating.Comment);
        }

        [Fact]
        public void CreateRating_ValidData_CreteRatingAndReturnsNewObject()
        {
            UserUtils.InitUser();
            var service = new RatingService();
            var rating = service.CreateRating("test", "tt10111746", 6);
            Assert.Equal("test", rating.Username);
            Assert.Equal("tt10111746", rating.TitleId);
            Assert.Equal(6, rating.Rate);
            Assert.Null(rating.Comment);

            // cleanup
            UserUtils.DeleteUser();
            service.DeleteRating(rating.Username, rating.TitleId);
        }
        
        [Fact]
        public void CreateRating_ValidDataButAlreadyExisting_ReturnsNullObject()
        {
            UserUtils.InitUser();
            var service = new RatingService();
            var rating = service.CreateRating("test", "tt10111746", 3);
            var sameRating = service.CreateRating("test", "tt10111746", 4);
            Assert.Null(sameRating);

            // cleanup
            UserUtils.DeleteUser();
            service.DeleteRating(rating.Username, rating.TitleId);
        }
        
        [Fact]
        public void GetAllRatings_ValidUsernameAndBasicPage_ReturnsFirstPage()
        {
            UserUtils.InitUser();
            var service = new RatingService();
            var rating1 = service.CreateRating("test", "tt10111746", 3);
            var rating2 = service.CreateRating("test", "tt12490740", 4);
            var rating3 = service.CreateRating("test", "tt1310664", 5);
            var rating4 = service.CreateRating("test", "tt1220221", 6);
            var rating5 = service.CreateRating("test", "tt0454854", 7);
            var ratings = service.GetRatings("test", 0, 10);
            Assert.Equal(5, ratings.Count);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteRating(rating1.Username, rating1.TitleId);
            service.DeleteRating(rating2.Username, rating2.TitleId);
            service.DeleteRating(rating3.Username, rating3.TitleId);
            service.DeleteRating(rating4.Username, rating4.TitleId);
            service.DeleteRating(rating5.Username, rating5.TitleId);
        }
        
        [Fact]
        public void GetAllRatings_ValidUsernameAndOutsidePage_ReturnsEmptyList()
        {
            UserUtils.InitUser();
            var service = new RatingService();
            var rating1 = service.CreateRating("test", "tt10111746", 3);
            var rating2 = service.CreateRating("test", "tt12490740", 4);
            var rating3 = service.CreateRating("test", "tt1310664", 5);
            var rating4 = service.CreateRating("test", "tt1220221", 6);
            var rating5 = service.CreateRating("test", "tt0454854", 7);
            var ratings = service.GetRatings("test", 1, 10);
            Assert.Equal(0, ratings.Count);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteRating(rating1.Username, rating1.TitleId);
            service.DeleteRating(rating2.Username, rating2.TitleId);
            service.DeleteRating(rating3.Username, rating3.TitleId);
            service.DeleteRating(rating4.Username, rating4.TitleId);
            service.DeleteRating(rating5.Username, rating5.TitleId);
        }
        
        [Fact]
        public void GetAllRatings_InValidUsername_ReturnsEmptyList()
        {
            UserUtils.InitUser();
            var service = new RatingService();
            var rating1 = service.CreateRating("test", "tt10111746", 3);
            var rating2 = service.CreateRating("test", "tt12490740", 4);
            var rating3 = service.CreateRating("test", "tt1310664", 5);
            var rating4 = service.CreateRating("test", "tt1220221", 6);
            var rating5 = service.CreateRating("test", "tt0454854", 7);
            var ratings = service.GetRatings("test2", 0, 10);
            Assert.Equal(0, ratings.Count);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteRating(rating1.Username, rating1.TitleId);
            service.DeleteRating(rating2.Username, rating2.TitleId);
            service.DeleteRating(rating3.Username, rating3.TitleId);
            service.DeleteRating(rating4.Username, rating4.TitleId);
            service.DeleteRating(rating5.Username, rating5.TitleId);
        }
        
        [Fact]
        public void GetRating_ValidUsernameAndTitleId_ReturnsRatingObject()
        {
            UserUtils.InitUser();
            var service = new RatingService();
            var createRating = service.CreateRating("test", "tt0327418", 3);
            Assert.NotNull(createRating);
            var rating = service.GetRating("test", "tt0327418");
            Assert.Equal("test", rating.Username);
            Assert.Equal("tt0327418", rating.TitleId);
            Assert.Equal(3, rating.Rate);
            Assert.Null(rating.Comment);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteRating(rating.Username, rating.TitleId);
        }
        
        [Fact]
        public void GetRating_InvalidUsername_ReturnsNullObject()
        {
            UserUtils.InitUser();
            var service = new RatingService();
            var createRating = service.CreateRating("test", "tt0327418", 3);
            Assert.NotNull(createRating);
            var ratings = service.GetRating("notExist", "tt0327418");
            Assert.Null(ratings);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteRating(createRating.Username, createRating.TitleId);
        }
        
        [Fact]
        public void GetRating_InvalidTitleId_ReturnsNullObject()
        {
            UserUtils.InitUser();
            var service = new RatingService();
            var createRating = service.CreateRating("test", "tt0327418", 3);
            Assert.NotNull(createRating);
            var ratings = service.GetRating("test", "notExist");
            Assert.Null(ratings);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteRating(createRating.Username, createRating.TitleId);
        }

        [Fact]
        public void DeleteRating_ValidUsernameAndTitleId_RemoveTheRating()
        {
            UserUtils.InitUser();
            var service = new RatingService();
            var rating = service.CreateRating("test", "tt10111746", 5);
            var result = service.DeleteRating(rating.Username, rating.TitleId);
            Assert.True(result);
            rating = service.GetRating(rating.Username, rating.TitleId);
            Assert.Null(rating);
            
            // cleanup
            UserUtils.DeleteUser();
        }

        [Fact]
        public void DeleteRating_InvalidUsername_ReturnsFalse()
        {
            UserUtils.InitUser();
            var service = new RatingService();
            var rating = service.CreateRating("test", "tt10111746", 5);
            var result = service.DeleteRating("notExist", "tt10111746");
            Assert.False(result);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteRating(rating.Username, rating.TitleId);
        }
        
        [Fact]
        public void DeleteRating_InvalidTitleId_ReturnsFalse()
        {
            UserUtils.InitUser();
            var service = new RatingService();
            var rating = service.CreateRating("test", "tt10111746", 5);
            var result = service.DeleteRating("test", "notExist");
            Assert.False(result);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteRating(rating.Username, rating.TitleId);
        }
        
        [Fact]
        public void UpdateRating_NewRateAndComment_UpdateWithNewValues()
        {
            UserUtils.InitUser();
            var service = new RatingService();
            var rating = service.CreateRating("test", "tt10111746", 2);

            var result = service.UpdateRating(rating.Username, rating.TitleId, 4, "UpdatedComment");
            Assert.True(result);

            rating = service.GetRating(rating.Username, rating.TitleId);

            Assert.Equal(4, rating.Rate);
            Assert.Equal("UpdatedComment", rating.Comment);

            // cleanup
            UserUtils.DeleteUser();
            service.DeleteRating(rating.Username, rating.TitleId);
        }
        
        [Fact]
        public void UpdateRating_InvalidUsername_ReturnsFalse()
        {
            UserUtils.InitUser();
            var service = new RatingService();
            var rating = service.CreateRating("test", "tt10111746", 5);
            var result = service.UpdateRating("notExist", "tt10111746", 10, "UpdateComment");
            Assert.False(result);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteRating(rating.Username, rating.TitleId);
        }
        
        [Fact]
        public void UpdateRating_InvalidTitleId_ReturnsFalse()
        {
            UserUtils.InitUser();
            var service = new RatingService();
            var rating = service.CreateRating("test", "tt10111746", 5);
            var result = service.UpdateRating("test", "notExist", 10, "UpdateComment");
            Assert.False(result);
            
            // cleanup
            UserUtils.DeleteUser();
            service.DeleteRating(rating.Username, rating.TitleId);
        }
    }
}