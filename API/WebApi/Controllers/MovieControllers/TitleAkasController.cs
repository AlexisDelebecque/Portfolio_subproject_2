using Microsoft.AspNetCore.Mvc;
using WebApi.Services.MovieServices;

namespace WebApi.Controllers.MovieControllers
{
    [ApiController]
    [Route(BaseTitleAkasRoute)]
    public class TitleAkasController : Controller
    {
        private const string BaseTitleAkasRoute = "api/title/akas";
        private readonly MovieBusinessLayer _movieBusinessLayer;

        public TitleAkasController()
        {
            _movieBusinessLayer = new MovieBusinessLayer();
        }

        [HttpGet]
        public IActionResult GetNameBasics()
        {
            var titleAkas = _movieBusinessLayer.GetTitleAkas();
            return Ok(titleAkas);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetNameBasic(int id)
        {
            var titleAkas = _movieBusinessLayer.GetTitleAka(id);

            if (titleAkas == null)
                return NotFound();

            return Ok(titleAkas);
        }
    }
}
