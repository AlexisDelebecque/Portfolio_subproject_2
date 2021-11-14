using System;
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
    public class NameBookmarksController: APagesController
    {
        private const string BaseUserRoute = "api/users/namebookmarks";
        private readonly UserBusinessLayer _userService;

        public NameBookmarksController(LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _userService = new UserBusinessLayer();
        }

        [HttpGet(Name = nameof(GetNameBookmarks))]
        public IActionResult GetNameBookmarks([FromQuery]PagesQueryString pagesQueryString)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var nameBookmarks = _nameBookmarkService
                    .GetNameBookmarks(user.Username, pagesQueryString.Page, pagesQueryString.PageSize);
                return Ok(CreatePagingResult(
                    pagesQueryString.Page,
                    pagesQueryString.PageSize,
                    _nameBookmarkService.CountNameBookmarks(user.Username),
                    nameBookmarks,
                    nameof(GetNameBookmarks)
                ));
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpGet("{nameId}")]
        public IActionResult GetNameBookmark(string nameId)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                return Ok(_userService.GetNameBookmark(user.Username, nameId));
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpPost]
        public IActionResult CreateNameBookmark(CreationNameBookmarkDto dto)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var rating = _userService.CreateNameBookmark(user.Username, dto.NameId);
                
                return Created($"{BaseUserRoute}/{rating.NameId}", rating);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpDelete("{nameId}")]
        public IActionResult DeleteNameBookmark(string nameId)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var isSucceeded = _userService.DeleteNameBookmark(user.Username, nameId);

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