using Microsoft.AspNetCore.Mvc;
using WebApi.Services.Movie;

namespace WebApi.Controllers
{
    [ApiController]
    [Route(baseNameBasicsRoute)]
    public class NameBasicsController : Controller
    {
        private const string baseNameBasicsRoute = "title/name";
        private BusinessLayer _businessLayer;

        public NameBasicsController()
        {
            _businessLayer = new BusinessLayer();
        }

        [HttpGet]
        public IActionResult GetNameBasics()
        {
            var nameBasics = _businessLayer.GetNameBasics();
            return Ok(nameBasics);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetNameBasic(int id)
        {
            var nameBasics = _businessLayer.GetNameBasic(id);

            if (nameBasics == null)
                return NotFound();

            return Ok(nameBasics);
        }
    }
}
