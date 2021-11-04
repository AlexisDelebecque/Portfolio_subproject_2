using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route(baseTitleBasicsRoute)]
    public class TitleBasicsController : Controller
    {
        private const string baseTitleBasicsRoute = "api/titleBasics"; //Han kaldte den anden for titles/episodes, måske have samme struktur, men Henrik virkede tvivlende. 
        private TitleBasicsService _titleBasicsService;
        public TitleBasicsController()
        {
            _titleBasicsService = new TitleBasicsService();
        }

        [HttpGet]
        public IActionResult GetTitleEpisodes()
        {
            var titleEpisodes = _titleBasicsService.GetTitleBasics();
            return Ok(titleEpisodes);
        }
    }
}
