using Microsoft.AspNetCore.Mvc;
using WebApi.Services.MovieServices;

namespace WebApi.Controllers.MovieControllers
{
    [ApiController]
    [Route(BaseWiRoute)]
    public class WiController : Controller
    {
        private const string BaseWiRoute = "title/wi";
        private readonly MovieBusinessLayer _movieBusinessLayer;

        public WiController()
        {
            _movieBusinessLayer = new MovieBusinessLayer();
        }

        [HttpGet]
        public IActionResult GetWis()
        {
            var wi = _movieBusinessLayer.GetWis();
            return Ok(wi);
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
