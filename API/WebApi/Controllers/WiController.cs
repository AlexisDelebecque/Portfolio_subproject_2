using Microsoft.AspNetCore.Mvc;
using WebApi.Services.Movie;

namespace WebApi.Controllers
{
    [ApiController]
    [Route(baseWiRoute)]
    public class WiController : Controller
    {
        private const string baseWiRoute = "title/wi";
        private BusinessLayer _businessLayer;

        public WiController()
        {
            _businessLayer = new BusinessLayer();
        }

        [HttpGet]
        public IActionResult GetWis()
        {
            var wi = _businessLayer.GetWis();
            return Ok(wi);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetWi(int id)
        {
            var wi = _businessLayer.GetWi(id);

            if (wi == null)
                return NotFound();

            return Ok(wi);
        }
    }
}
