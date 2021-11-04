using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route(baseTitleEpisodesRoute)]
    public class TitleEpisodesController: Controller
    {
        private const string baseTitleEpisodesRoute = "api/title/episodes";
        private TitleEpisodesService _titleEpisodeService;

        public TitleEpisodesController()
        {
            _titleEpisodeService = new TitleEpisodesService();
        }
        
        [HttpGet]
        public IActionResult GetTitleEpisodes()
        {
            var titleEpisodes = _titleEpisodeService.GetTitleEpisodes();
            return Ok(titleEpisodes);
        }
    }
}