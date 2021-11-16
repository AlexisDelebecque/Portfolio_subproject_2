using Microsoft.AspNetCore.Mvc;
using WebApi.Services.MovieServices;

namespace WebApi.Controllers.MovieControllers
{
    [ApiController]
    [Route(BaseTitleCrewRoute)]
    public class TitleCrewController : Controller
    {
        private const string BaseTitleCrewRoute = "api/title/crews";
        private readonly MovieBusinessLayer _movieBusinessLayer;

        public TitleCrewController()
        {
            _movieBusinessLayer = new MovieBusinessLayer();
        }

        [HttpGet]
        public IActionResult GetTitleCrews()
        {
            var titleCrew = _movieBusinessLayer.GetTitleCrews();
            return Ok(titleCrew);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetTitleCrew(int id)
        {
            var titleCrew = _movieBusinessLayer.GetTitleCrew(id);

            if (titleCrew == null)
                return NotFound();

            return Ok(titleCrew);
        }
    }
}
