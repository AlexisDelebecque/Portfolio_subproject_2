using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;
using WebApi.Domain.UserDomain;
using WebApi.Services.UserServices;
using WebApi.ViewModels;

namespace WebApi.Controllers.UserControllers
{
    [Authorization]
    [ApiController]
    [Route(BaseUserRoute)]
    public class RatingsController: ControllerBase
    {
        private const string BaseUserRoute = "api/users/ratings";
        private readonly RatingService _ratingService;

        public RatingsController()
        {
            _ratingService = new RatingService();
        }

        [HttpGet]
        public IActionResult GetRatings()
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                return Ok(_ratingService.GetRatings(user.Username));
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpGet("{titleId}")]
        public IActionResult GetRating(string titleId)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                return Ok(_ratingService.GetRating(user.Username, titleId));
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpPost]
        public IActionResult CreateRating(CreationRatingDto dto)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var rating = _ratingService.CreateRating(user.Username, dto.TitleId, dto.Rate, dto.Comment);
                
                return Created($"{BaseUserRoute}/{rating.TitleId}", rating);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpPut("{titleId}")]
        public IActionResult UpdateRating(string titleId, UpdateRatingDto dto)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var isSucceeded = _ratingService.UpdateRating(user.Username, titleId, dto.Rate, dto.Comment);

                if (!isSucceeded)
                    return NotFound();
                return Ok();
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpDelete("{titleId}")]
        public IActionResult DeleteRating(string titleId)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var isSucceeded = _ratingService.DeleteRating(user.Username, titleId);

                if (!isSucceeded)
                    return NotFound();
                return Ok();
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
    }
}