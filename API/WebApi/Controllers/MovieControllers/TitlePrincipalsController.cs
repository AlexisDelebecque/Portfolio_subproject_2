using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApi.Services.MovieServices;

namespace WebApi.Controllers.MovieControllers
{
    [ApiController]
    [Route(BaseTitlePrincipalsRoute)]
    public class TitlePrincipalsController : APagesController
    {
        private const string BaseTitlePrincipalsRoute = "api/title/principals";
        private readonly MovieBusinessLayer _movieBusinessLayer;

        public TitlePrincipalsController(LinkGenerator linkGenerator): base(linkGenerator)
        {
            _movieBusinessLayer = new MovieBusinessLayer();
        }

        [HttpGet(Name = nameof(GetTitlePrincipals))]
        public IActionResult GetTitlePrincipals([FromQuery]PagesQueryString pagesQueryString)
        {
            var titlePrincipals = _movieBusinessLayer
                .GetTitlePrincipals(pagesQueryString.Page, pagesQueryString.PageSize);
            return Ok(CreatePagingResult(
                pagesQueryString.Page,
                pagesQueryString.PageSize,
                _movieBusinessLayer.CountTitlePrincipals(),
                titlePrincipals,
                nameof(GetTitlePrincipals)
            ));
        }


        [HttpGet("{id:int}")]
        public IActionResult GetTitlePrincipal(int id)
        {
            var titlePrincipals = _movieBusinessLayer.GetTitlePrincipal(id);

            if (titlePrincipals == null)
                return NotFound();

            return Ok(titlePrincipals);
        }
    }
}
