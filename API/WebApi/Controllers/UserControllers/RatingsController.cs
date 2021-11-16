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
        private readonly UserBusinessLayer _userService;

        public RatingsController()
        {
            _userService = new UserBusinessLayer();
        }

        [HttpGet]
        public IActionResult GetRatings()
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                return Ok(_userService.GetRatings(user.Username));
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
                return Ok(_userService.GetRating(user.Username, titleId));
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
                var rating = _userService.CreateRating(user.Username, dto.TitleId, dto.Rate, dto.Comment);
                
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
                var isSucceeded = _userService.UpdateRating(user.Username, titleId, dto.Rate, dto.Comment);

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
                var isSucceeded = _userService.DeleteRating(user.Username, titleId);

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