using Microsoft.AspNetCore.Mvc;
using WebApi.Services.Movie;

namespace WebApi.Controllers
{
    [ApiController]
    [Route(baseTitleCrewRoute)]
    public class TitleCrewController : Controller
    {
        private const string baseTitleCrewRoute = "title/crews";
        private BusinessLayer _businessLayer;

        public TitleCrewController()
        {
            _businessLayer = new BusinessLayer();
        }

        [HttpGet]
        public IActionResult GetTitleCrews()
        {
            var titleCrew = _businessLayer.GetTitleCrews();
            return Ok(titleCrew);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetTitleCrew(int id)
        {
            var titleCrew = _businessLayer.GetTitleCrew(id);

            if (titleCrew == null)
                return NotFound();

            return Ok(titleCrew);
        }
    }
}
