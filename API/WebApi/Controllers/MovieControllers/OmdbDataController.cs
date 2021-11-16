using Microsoft.AspNetCore.Mvc;
using WebApi.Services.MovieServices;

namespace WebApi.Controllers.MovieControllers
{
    [ApiController]
    [Route(BaseOmdbDataRoute)]
    public class OmdbDataController : Controller
    {
        private const string BaseOmdbDataRoute = "api/title/omdb";
        private readonly MovieBusinessLayer _movieBusinessLayer;

        public OmdbDataController()
        {
            _movieBusinessLayer = new MovieBusinessLayer();
        }

        [HttpGet]
        public IActionResult GetOmdbDatas()
        {
            var omdbDatas = _movieBusinessLayer.GetOmdbDatas();
            return Ok(omdbDatas);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOmdbData(int id)
        {
            var omdbDatas = _movieBusinessLayer.GetOmdbData(id);

            if (omdbDatas == null)
                return NotFound();

            return Ok(omdbDatas);
        }
    }
}
