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
    public class SearchHistoriesController: ControllerBase
    {
        private const string BaseUserRoute = "api/users/searchhistories";
        private readonly UserBusinessLayer _userService;

        public SearchHistoriesController()
        {
            _userService = new UserBusinessLayer();
        }

        [HttpGet]
        public IActionResult GetSearchHistories()
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                return Ok(_userService.GetSearchHistories(user.Username));
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpGet("{searchKey}")]
        public IActionResult GetSearchHistory(string searchKey)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                return Ok(_userService.GetSearchHistory(user.Username, searchKey));
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpPost]
        public IActionResult CreateSearchHistory(CreationSearchHistoryDto dto)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var rating = _userService.CreateSearchHistory(user.Username, dto.SearchKey);
                
                return Created($"{BaseUserRoute}/{rating.SearchKey}", rating);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpDelete("{searchKey}")]
        public IActionResult DeleteSearchHistory(string searchKey)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var isSucceeded = _userService.DeleteSearchHistory(user.Username, searchKey);

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