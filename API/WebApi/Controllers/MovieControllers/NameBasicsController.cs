using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApi.Services.MovieServices;

namespace WebApi.Controllers.MovieControllers
{
    [ApiController]
    [Route(BaseNameBasicsRoute)]
    public class NameBasicsController : APagesController
    {
        private const string BaseNameBasicsRoute = "api/title/name";
        private readonly MovieBusinessLayer _movieBusinessLayer;

        public NameBasicsController(LinkGenerator linkGenerator): base(linkGenerator)
        {
            _movieBusinessLayer = new MovieBusinessLayer();
        }

        [HttpGet(Name = nameof(GetNameBasics))]
        public IActionResult GetNameBasics([FromQuery]PagesQueryString pagesQueryString)
        {
            var nameBasics = _movieBusinessLayer
                .GetNameBasics(pagesQueryString.Page, pagesQueryString.PageSize);
            return Ok(CreatePagingResult(
                pagesQueryString.Page,
                pagesQueryString.PageSize,
                _movieBusinessLayer.CountNameBasics(),
                nameBasics,
                nameof(GetNameBasics)
            ));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetNameBasic(int id)
        {
            var nameBasics = _movieBusinessLayer.GetNameBasic(id);

            if (nameBasics == null)
                return NotFound();

            return Ok(nameBasics);
        }
    }
}
