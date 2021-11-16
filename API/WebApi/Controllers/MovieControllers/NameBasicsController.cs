using Microsoft.AspNetCore.Mvc;
using WebApi.Services.MovieServices;

namespace WebApi.Controllers.MovieControllers
{
    [ApiController]
    [Route(BaseNameBasicsRoute)]
    public class NameBasicsController : Controller
    {
        private const string BaseNameBasicsRoute = "api/title/name";
        private readonly MovieBusinessLayer _movieBusinessLayer;

        public NameBasicsController()
        {
            _movieBusinessLayer = new MovieBusinessLayer();
        }

        [HttpGet]
        public IActionResult GetNameBasics()
        {
            var nameBasics = _movieBusinessLayer.GetNameBasics();
            return Ok(nameBasics);
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
