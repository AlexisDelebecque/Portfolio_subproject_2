using Microsoft.AspNetCore.Mvc;
using WebApi.Services.Movie;

namespace WebApi.Controllers
{
    [ApiController]
    [Route(baseTitleAkasRoute)]
    public class TitleAkasController : Controller
    {
        private const string baseTitleAkasRoute = "title/akas";
        private BusinessLayer _businessLayer;

        public TitleAkasController()
        {
            _businessLayer = new BusinessLayer();
        }

        [HttpGet]
        public IActionResult GetNameBasics()
        {
            var titleAkas = _businessLayer.GetTitleAkas();
            return Ok(titleAkas);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetNameBasic(int id)
        {
            var titleAkas = _businessLayer.GetTitleAka(id);

            if (titleAkas == null)
                return NotFound();

            return Ok(titleAkas);
        }
    }
}
