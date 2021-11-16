using Microsoft.AspNetCore.Mvc;
using WebApi.Services.MovieServices;

namespace WebApi.Controllers.MovieControllers
{
    [ApiController]
    [Route(BaseTitleRatingsRoute)]
    public class TitleRatingController : Controller
    {
        private const string BaseTitleRatingsRoute = "api/title/ratings";
        private readonly MovieBusinessLayer _movieBusinessLayer;

        public TitleRatingController()
        {
            _movieBusinessLayer = new MovieBusinessLayer();
        }

        [HttpGet]
        public IActionResult GetTitleRatings()
        {
            var titleRatings = _movieBusinessLayer.GetTitleRatings();
            return Ok(titleRatings);
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
