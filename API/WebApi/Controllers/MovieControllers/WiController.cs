using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApi.Services.MovieServices;

namespace WebApi.Controllers.MovieControllers
{
    [ApiController]
    [Route(BaseWiRoute)]
    public class WiController : APagesController
    {
        private const string BaseWiRoute = "api/title/wi";
        private readonly MovieBusinessLayer _movieBusinessLayer;

        public WiController(LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _movieBusinessLayer = new MovieBusinessLayer();
        }

        [HttpGet(Name = nameof(GetWis))]
        public IActionResult GetWis([FromQuery]PagesQueryString pagesQueryString)
        {
            var wis = _movieBusinessLayer
                .GetWis(pagesQueryString.Page, pagesQueryString.PageSize);
            return Ok(CreatePagingResult(
                pagesQueryString.Page,
                pagesQueryString.PageSize,
                _movieBusinessLayer.CountWis(),
                wis,
                nameof(GetWis)
            ));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetWi(int id)
        {
            var wi = _movieBusinessLayer.GetWi(id);

            if (wi == null)
                return NotFound();

            return Ok(wi);
        }
    }
}
