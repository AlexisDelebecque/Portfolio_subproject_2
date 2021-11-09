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
    public class TitleBookmarksController: ControllerBase
    {
        private const string BaseUserRoute = "api/users/titlebookmarks";
        private readonly TitleBookmarkService _titleBookmarkService;

        public TitleBookmarksController()
        {
            _titleBookmarkService = new TitleBookmarkService();
        }

        [HttpGet]
        public IActionResult GetTitleBookmarks()
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                return Ok(_titleBookmarkService.GetTitleBookmarks(user.Username));
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
                return Ok(_titleBookmarkService.GetTitleBookmark(user.Username, titleId));
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
                var rating = _titleBookmarkService.CreateTitleBookmark(user.Username, dto.TitleId);
                
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
                var isSucceeded = _titleBookmarkService.DeleteTitleBookmark(user.Username, titleId);

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