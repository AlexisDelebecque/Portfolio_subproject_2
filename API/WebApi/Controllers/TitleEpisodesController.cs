using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route(BaseTitleEpisodesRoute)]
    public class TitleEpisodesController: ControllerBase
    {
        private const string BaseTitleEpisodesRoute = "api/title/episodes";
        private TitleEpisodesService _titleEpisodeService;

        public TitleEpisodesController()
        {
            _titleEpisodeService = new TitleEpisodesService();
        }
        
        [Authorization]
        [HttpGet]
        public IActionResult GetTitleEpisodes()
        {
            var titleEpisodes = _titleEpisodeService.GetTitleEpisodes();
            return Ok(titleEpisodes);
        }
    }
}