using Microsoft.AspNetCore.Mvc;
using WebApi.Services.Movie;

namespace WebApi.Controllers
{
    [ApiController]
    [Route(baseTitlePrincipalsRoute)]
    public class TitlePrincipalsController : Controller
    {
        private const string baseTitlePrincipalsRoute = "title/principals";
        private BusinessLayer _businessLayer;

        public TitlePrincipalsController()
        {
            _businessLayer = new BusinessLayer();
        }

        [HttpGet]
        public IActionResult GetTitlePrincipals()
        {
            var titlePrincipals = _businessLayer.GetTitlePrincipals();
            return Ok(titlePrincipals);
        }


        [HttpGet("{id:int}")]
        public IActionResult GetTitlePrincipal(int id)
        {
            var titlePrincipals = _businessLayer.GetTitlePrincipal(id);

            if (titlePrincipals == null)
                return NotFound();

            return Ok(titlePrincipals);
        }
    }
}
