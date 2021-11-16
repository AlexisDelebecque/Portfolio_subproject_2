using System;
using System.Collections.Generic;
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
    public class TitleBookmarksController: APagesController
    {
        private const string BaseUserRoute = "api/users/titlebookmarks";
        private readonly UserBusinessLayer _userService;

        public TitleBookmarksController(LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _userService = new UserBusinessLayer();
        }

        [HttpGet(Name = nameof(GetTitleBookmarks))]
        public IActionResult GetTitleBookmarks([FromQuery]PagesQueryString pagesQueryString)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var titleBookmarks = _userService
                    .GetTitleBookmarks(user.Username, pagesQueryString.Page, pagesQueryString.PageSize);
                return Ok(CreatePagingResult(
                    pagesQueryString.Page,
                    pagesQueryString.PageSize,
                    _userService.CountTitleBookmarks(user.Username),
                    titleBookmarks,
                    nameof(GetTitleBookmarks)
                ));
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpGet("{titleId}")]
        public IActionResult GetTitleBookmark(string titleId)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                return Ok(_userService.GetTitleBookmark(user.Username, titleId));
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpPost]
        public IActionResult CreateTitleBookmark(CreationTitleBookmarkDto dto)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var rating = _userService.CreateTitleBookmark(user.Username, dto.TitleId);
                
                return Created($"{BaseUserRoute}/{rating.TitleId}", rating);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpDelete("{titleId}")]
        public IActionResult DeleteTitleBookmark(string titleId)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var isSucceeded = _userService.DeleteTitleBookmark(user.Username, titleId);

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