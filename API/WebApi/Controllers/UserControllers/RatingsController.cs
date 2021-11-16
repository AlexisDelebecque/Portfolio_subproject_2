using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApi.Attributes;
using WebApi.Domain.UserDomain;
using WebApi.Services.UserServices;
using WebApi.Utils;
using WebApi.ViewModels;

namespace WebApi.Controllers.UserControllers
{
    [Authorization]
    [ApiController]
    [Route(BaseUserRoute)]
    public class RatingsController: APagesController
    {
        private const string BaseUserRoute = "api/users/ratings";
        private readonly UserBusinessLayer _userService;

        public RatingsController(LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _userService = new UserBusinessLayer();
        }

        [HttpGet(Name = nameof(GetRatings))]
        public IActionResult GetRatings([FromQuery]PagesQueryString pagesQueryString)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var ratings = _userService
                    .GetRatings(user.Username, pagesQueryString.Page, pagesQueryString.PageSize);
                return Ok(CreatePagingResult(
                    pagesQueryString.Page,
                    pagesQueryString.PageSize,
                    _userService.CountRatings(user.Username),
                    ratings,
                    nameof(GetRatings)
                ));
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