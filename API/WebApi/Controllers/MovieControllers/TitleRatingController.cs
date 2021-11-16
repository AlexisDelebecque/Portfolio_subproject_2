using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApi.Services.MovieServices;

namespace WebApi.Controllers.MovieControllers
{
    [ApiController]
    [Route(BaseTitleRatingsRoute)]
    public class TitleRatingController : APagesController
    {
        private const string BaseTitleRatingsRoute = "api/title/ratings";
        private readonly MovieBusinessLayer _movieBusinessLayer;

        public TitleRatingController(LinkGenerator linkGenerator): base(linkGenerator)
        {
            _movieBusinessLayer = new MovieBusinessLayer();
        }

        [HttpGet(Name = nameof(GetTitleRatings))]
        public IActionResult GetTitleRatings([FromQuery]PagesQueryString pagesQueryString)
        {
            var titleRatings = _movieBusinessLayer
                .GetTitleRatings(pagesQueryString.Page, pagesQueryString.PageSize);
            return Ok(CreatePagingResult(
                pagesQueryString.Page,
                pagesQueryString.PageSize,
                _movieBusinessLayer.CountTitleRatings(),
                titleRatings,
                nameof(GetTitleRatings)
            ));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetTitleRating(int id)
        {
            var titleRatings = _movieBusinessLayer.GetTitleRating(id);

            if (titleRatings == null)
                return NotFound();

            return Ok(titleRatings);
        }
    }
}
