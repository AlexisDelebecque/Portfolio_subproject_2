using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;
using WebApi.Services.Movie;

namespace WebApi.Controllers
{
    [ApiController]
    [Route(BaseTitleEpisodesRoute)]
    public class TitleEpisodesController: ControllerBase
    {
        private const string BaseTitleEpisodesRoute = "api/title/episodes";
        private BusinessLayer _businessLayer;

        public TitleEpisodesController()
        {
            _businessLayer = new BusinessLayer();
        }
        
        [Authorization]
        [HttpGet]
        public IActionResult GetTitleEpisodes()
        {
            var titleEpisodes = _businessLayer.GetTitleEpisodes();
            return Ok(titleEpisodes);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetTitleEpisode(int id)
        {
            var titleEpisode = _businessLayer.GetTitleEpisode(id);

            if (titleEpisode == null)
                return NotFound();

            return Ok(titleEpisode);
        }
    }
}