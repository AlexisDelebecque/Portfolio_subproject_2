using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApi.Services.MovieServices;

namespace WebApi.Controllers.MovieControllers
{
    [ApiController]
    [Route(BaseTitleCrewRoute)]
    public class TitleCrewController : APagesController
    {
        private const string BaseTitleCrewRoute = "api/title/crews";
        private readonly MovieBusinessLayer _movieBusinessLayer;

        public TitleCrewController(LinkGenerator linkGenerator): base(linkGenerator)
        {
            _movieBusinessLayer = new MovieBusinessLayer();
        }

        [HttpGet(Name = nameof(GetTitleCrews))]
        public IActionResult GetTitleCrews([FromQuery]PagesQueryString pagesQueryString)
        {
            var titleCrew = _movieBusinessLayer
                .GetTitleCrews(pagesQueryString.Page, pagesQueryString.PageSize);
            return Ok(CreatePagingResult(
                pagesQueryString.Page,
                pagesQueryString.PageSize,
                _movieBusinessLayer.CountTitleCrews(),
                titleCrew,
                nameof(GetTitleCrews)
            ));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetTitleCrew(int id)
        {
            var titleCrew = _movieBusinessLayer.GetTitleCrew(id);

            if (titleCrew == null)
                return NotFound();

            return Ok(titleCrew);
        }
    }
}
