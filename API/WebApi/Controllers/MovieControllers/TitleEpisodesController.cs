using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;
using WebApi.Services.MovieServices;

namespace WebApi.Controllers.MovieControllers
{
    [ApiController]
    [Route(BaseTitleEpisodesRoute)]
    public class TitleEpisodesController: ControllerBase
    {
        private const string BaseTitleEpisodesRoute = "api/title/episodes";
        private readonly MovieBusinessLayer _movieBusinessLayer;

        public TitleEpisodesController()
        {
            _movieBusinessLayer = new MovieBusinessLayer();
        }
        
        [Authorization]
        [HttpGet]
        public IActionResult GetTitleEpisodes()
        {
            var titleEpisodes = _movieBusinessLayer.GetTitleEpisodes();
            return Ok(titleEpisodes);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetTitleEpisode(int id)
        {
            var titleEpisode = _movieBusinessLayer.GetTitleEpisode(id);

            if (titleEpisode == null)
                return NotFound();

            return Ok(titleEpisode);
        }
    }
}