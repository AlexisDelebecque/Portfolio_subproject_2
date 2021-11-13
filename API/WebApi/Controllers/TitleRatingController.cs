using Microsoft.AspNetCore.Mvc;
using WebApi.Services.Movie;

namespace WebApi.Controllers
{
    [ApiController]
    [Route(baseTitleRatingsRoute)]
    public class TitleRatingController : Controller
    {
        private const string baseTitleRatingsRoute = "title/ratings";
        private BusinessLayer _businessLayer;

        public TitleRatingController()
        {
            _businessLayer = new BusinessLayer();
        }

        [HttpGet]
        public IActionResult GetTitleRatings()
        {
            var titleRatings = _businessLayer.GetTitleRatings();
            return Ok(titleRatings);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetTitleRating(int id)
        {
            var titleRatings = _businessLayer.GetTitleRating(id);

            if (titleRatings == null)
                return NotFound();

            return Ok(titleRatings);
        }
    }
}
