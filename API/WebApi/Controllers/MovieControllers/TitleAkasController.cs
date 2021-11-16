using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApi.Services.MovieServices;

namespace WebApi.Controllers.MovieControllers
{
    [ApiController]
    [Route(BaseTitleAkasRoute)]
    public class TitleAkasController : APagesController
    {
        private const string BaseTitleAkasRoute = "api/title/akas";
        private readonly MovieBusinessLayer _movieBusinessLayer;

        public TitleAkasController(LinkGenerator linkGenerator): base(linkGenerator)
        {
            _movieBusinessLayer = new MovieBusinessLayer();
        }

        [HttpGet(Name = nameof(GetTitleAkas))]
        public IActionResult GetTitleAkas([FromQuery]PagesQueryString pagesQueryString)
        {
            var titleAkas = _movieBusinessLayer
                .GetTitleAkas(pagesQueryString.Page, pagesQueryString.PageSize);
            return Ok(CreatePagingResult(
                pagesQueryString.Page,
                pagesQueryString.PageSize,
                _movieBusinessLayer.CountTitleAkas(),
                titleAkas,
                nameof(GetTitleAkas)
            ));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetTitleAkas(int id)
        {
            var titleAkas = _movieBusinessLayer.GetTitleAka(id);

            if (titleAkas == null)
                return NotFound();

            return Ok(titleAkas);
        }
    }
}
