using Microsoft.AspNetCore.Mvc;
using WebApi.Services.MovieServices;

namespace WebApi.Controllers.MovieControllers
{
    [ApiController]
    [Route(BaseTitleBasicsRoute)]
    public class TitleBasicsController : Controller
    {
        private const string BaseTitleBasicsRoute = "api/title/basics";
        private readonly MovieBusinessLayer _movieBusinessLayer;

        public TitleBasicsController()
        {
            _movieBusinessLayer = new MovieBusinessLayer();
        }

        [HttpGet]
        public IActionResult GetTitleBasics()
        {
            var titleBasics = _movieBusinessLayer.GetTitleBasics();
            return Ok(titleBasics);
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
