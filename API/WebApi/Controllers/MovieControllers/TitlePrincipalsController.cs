using Microsoft.AspNetCore.Mvc;
using WebApi.Services.MovieServices;

namespace WebApi.Controllers.MovieControllers
{
    [ApiController]
    [Route(BaseTitlePrincipalsRoute)]
    public class TitlePrincipalsController : Controller
    {
        private const string BaseTitlePrincipalsRoute = "api/title/principals";
        private readonly MovieBusinessLayer _movieBusinessLayer;

        public TitlePrincipalsController()
        {
            _movieBusinessLayer = new MovieBusinessLayer();
        }

        [HttpGet]
        public IActionResult GetTitlePrincipals()
        {
            var titlePrincipals = _movieBusinessLayer.GetTitlePrincipals();
            return Ok(titlePrincipals);
        }


        [HttpGet("{id:int}")]
        public IActionResult GetTitlePrincipal(int id)
        {
            var titlePrincipals = _movieBusinessLayer.GetTitlePrincipal(id);

            if (titlePrincipals == null)
                return NotFound();

            return Ok(titlePrincipals);
        }
    }
}
