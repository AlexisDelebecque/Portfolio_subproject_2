using System;
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
    public class NameBookmarksController: ControllerBase
    {
        private const string BaseUserRoute = "api/users/namebookmarks";
        private readonly NameBookmarkService _nameBookmarkService;

        public NameBookmarksController()
        {
            _nameBookmarkService = new NameBookmarkService();
        }

        [HttpGet]
        public IActionResult GetNameBookmarks()
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                return Ok(_nameBookmarkService.GetNameBookmarks(user.Username));
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
                return Ok(_nameBookmarkService.GetNameBookmark(user.Username, nameId));
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
                var rating = _nameBookmarkService.CreateNameBookmark(user.Username, dto.NameId);
                
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
                var isSucceeded = _nameBookmarkService.DeleteNameBookmark(user.Username, nameId);

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