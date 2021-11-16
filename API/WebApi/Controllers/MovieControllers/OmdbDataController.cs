using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApi.Services.MovieServices;

namespace WebApi.Controllers.MovieControllers
{
    [ApiController]
    [Route(BaseOmdbDataRoute)]
    public class OmdbDataController : APagesController
    {
        private const string BaseOmdbDataRoute = "api/title/omdb";
        private readonly MovieBusinessLayer _movieBusinessLayer;

        public OmdbDataController(LinkGenerator linkGenerator): base(linkGenerator)
        {
            _movieBusinessLayer = new MovieBusinessLayer();
        }

        [HttpGet(Name = nameof(GetOmdbDatas))]
        public IActionResult GetOmdbDatas([FromQuery]PagesQueryString pagesQueryString)
        {
            var omdbDatas = _movieBusinessLayer
                .GetOmdbDatas(pagesQueryString.Page, pagesQueryString.PageSize);
            return Ok(CreatePagingResult(
                pagesQueryString.Page,
                pagesQueryString.PageSize,
                _movieBusinessLayer.CountOmdbDatas(),
                omdbDatas,
                nameof(GetOmdbDatas)
            ));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOmdbData(int id)
        {
            var omdbDatas = _movieBusinessLayer.GetOmdbData(id);

            if (omdbDatas == null)
                return NotFound();

            return Ok(omdbDatas);
        }
    }
}
