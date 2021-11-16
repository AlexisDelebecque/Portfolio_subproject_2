using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApi.Services.MovieServices;

namespace WebApi.Controllers.MovieControllers
{
    [ApiController]
    [Route(BaseTitleBasicsRoute)]
    public class TitleBasicsController : APagesController
    {
        private const string BaseTitleBasicsRoute = "api/title/basics";
        private readonly MovieBusinessLayer _movieBusinessLayer;

        public TitleBasicsController(LinkGenerator linkGenerator): base(linkGenerator)
        {
            _movieBusinessLayer = new MovieBusinessLayer();
        }

        [HttpGet(Name = nameof(GetTitleBasics))]
        public IActionResult GetTitleBasics([FromQuery]PagesQueryString pagesQueryString)
        {
            var titleBasics = _movieBusinessLayer
                .GetTitleBasics(pagesQueryString.Page, pagesQueryString.PageSize);
            return Ok(CreatePagingResult(
                pagesQueryString.Page,
                pagesQueryString.PageSize,
                _movieBusinessLayer.CountTitleBasics(),
                titleBasics,
                nameof(GetTitleBasics)
            ));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetTitleBasic(int id)
        {
            var titleBasic = _movieBusinessLayer.GetTitleBasic(id);

            if (titleBasic == null)
                return NotFound();

            return Ok(titleBasic);
        }
    }
}
