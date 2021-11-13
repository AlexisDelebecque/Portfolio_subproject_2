using Microsoft.AspNetCore.Mvc;
using WebApi.Services.Movie;

namespace WebApi.Controllers
{
    [ApiController]
    [Route(baseTitleBasicsRoute)]
    public class TitleBasicsController : Controller
    {
        private const string baseTitleBasicsRoute = "title/basics";
        private BusinessLayer _businessLayer;

        public TitleBasicsController()
        {
            _businessLayer = new BusinessLayer();
        }

        [HttpGet]
        public IActionResult GetTitleBasics()
        {
            var titleBasics = _businessLayer.GetTitleBasics();
            return Ok(titleBasics);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetTitleBasic(int id)
        {
            var titleBasic = _businessLayer.GetTitleBasic(id);

            if (titleBasic == null)
                return NotFound();

            return Ok(titleBasic);
        }
    }
}
