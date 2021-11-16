using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApi.Attributes;
using WebApi.Services.MovieServices;

namespace WebApi.Controllers.MovieControllers
{
    [ApiController]
    [Route(BaseTitleEpisodesRoute)]
    public class TitleEpisodesController: APagesController
    {
        private const string BaseTitleEpisodesRoute = "api/title/episodes";
        private readonly MovieBusinessLayer _movieBusinessLayer;

        public TitleEpisodesController(LinkGenerator linkGenerator): base(linkGenerator)
        {
            _movieBusinessLayer = new MovieBusinessLayer();
        }
        
        [HttpGet(Name = nameof(GetTitleEpisodes))]
        public IActionResult GetTitleEpisodes([FromQuery]PagesQueryString pagesQueryString)
        {
            var titleEpisodes = _movieBusinessLayer
                .GetTitleEpisodes(pagesQueryString.Page, pagesQueryString.PageSize);
            return Ok(CreatePagingResult(
                pagesQueryString.Page,
                pagesQueryString.PageSize,
                _movieBusinessLayer.CountTitleEpisodes(),
                titleEpisodes,
                nameof(GetTitleEpisodes)
            ));
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