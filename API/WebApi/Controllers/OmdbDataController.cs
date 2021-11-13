using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Services.Movie;

namespace WebApi.Controllers
{
    [ApiController]
    [Route(baseOmdbDataRoute)]
    public class OmdbDataController : Controller
    {
        private const string baseOmdbDataRoute = "title/omdb";
        private BusinessLayer _businessLayer;

        public OmdbDataController()
        {
            _businessLayer = new BusinessLayer();
        }

        [HttpGet]
        public IActionResult GetOmdbDatas()
        {
            var omdbDatas = _businessLayer.GetOmdbDatas();
            return Ok(omdbDatas);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOmdbData(int id)
        {
            var omdbDatas = _businessLayer.GetOmdbData(id);

            if (omdbDatas == null)
                return NotFound();

            return Ok(omdbDatas);
        }
    }
}
