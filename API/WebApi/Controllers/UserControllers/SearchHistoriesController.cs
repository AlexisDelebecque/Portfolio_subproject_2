using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApi.Attributes;
using WebApi.Domain.UserDomain;
using WebApi.Services.UserServices;
using WebApi.ViewModels;

namespace WebApi.Controllers.UserControllers
{
    [Authorization]
    [ApiController]
    [Route(BaseUserRoute)]
    public class SearchHistoriesController: APagesController
    {
        private const string BaseUserRoute = "api/users/searchhistories";
        private readonly UserBusinessLayer _userService;

        public SearchHistoriesController(LinkGenerator linkGenerator): base(linkGenerator)
        {
            _userService = new UserBusinessLayer();
        }

        [HttpGet(Name = nameof(GetSearchHistories))]
        public IActionResult GetSearchHistories([FromQuery]PagesQueryString pagesQueryString)
        {
            try
            {
                if (Request.HttpContext.Items["User"] is not User user)
                    throw new ArgumentException("User not exist");
                var searchHistories = _searchHistoryService
                    .GetSearchHistories(user.Username, pagesQueryString.Page, pagesQueryString.PageSize);
                return Ok(CreatePagingResult(
                    pagesQueryString.Page,
                    pagesQueryString.PageSize,
                    _searchHistoryService.CountSearchHistories(user.Username),
                    searchHistories,
                    nameof(GetSearchHistories)
                ));
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