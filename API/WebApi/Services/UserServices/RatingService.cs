using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.UserDomain;

namespace WebApi.Services.UserServices
{
    public class RatingService
    {
        private readonly PortfolioContext _ctx;

        public RatingService()
        {
            _ctx = new PortfolioContext();
        }

        public IList<Rating> GetRatings(string username)
        {
            return _ctx.Ratings
                .Where(x => x.Username == username)
                .ToList();
        }

        public Rating GetRating(string username, string titleId)
        {
            return _ctx.Ratings
                .FirstOrDefault(x => x.Username == username && x.TitleId == titleId);
        }
        
        public Rating CreateRating(string username, string titleId, int rate, string comment = null)
        {
            if (GetRating(username, titleId) != null)
                return null;
            
            var rating = new Rating
            {
                Username = username,
                TitleId = titleId,
                Rate = rate,
                Comment = comment,
            };

            _ctx.Ratings.Add(rating);
            _ctx.SaveChanges();
            return rating;
        }
        
        public bool DeleteRating(string username, string titleId)
        {
            var rating = GetRating(username, titleId);

            if (rating == null)
                return false;
            
            _ctx.Ratings.Remove(rating);
            return _ctx.SaveChanges() > 0;
        }

        public bool UpdateRating(string username, string titleId, int rate, string comment = null)
        {
            var rating = _ctx.Ratings.Find(username, titleId);

            if (rating == null)
                return false;

            rating.Rate = rate;
            rating.Comment = comment;
            
            return _ctx.SaveChanges() > 0;
        }
    }
}