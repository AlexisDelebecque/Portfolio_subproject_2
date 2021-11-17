using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Services.FunctionService;
using WebApi.ViewModels;


namespace WebApi.Controllers.FunctionsControllers
{
    [ApiController]
    [Route(BaseTitleEpisodesRoute)]

    public class FunctionController : ControllerBase
    {

        private const string BaseTitleEpisodesRoute = "api/function";
        private FunctionService _functionService;

        public FunctionController()
        {
            _functionService = new FunctionService();
        }

        /*
        [HttpGet("bestmatch")]
        //[HttpGet("{strings}")]

        //public IActionResult BestMatchSearch([FromRoute]string[] strings) //Cant figure out the multiple inputs
        public IActionResult BestMatchSearch([FromRoute] params string[] strings)

        {
            //var titleEpisodes = _functionService.BestMatchSearch(searchStrings);
            //return Ok(titleEpisodes);
            return Ok(strings);
        }
        */
        
        [HttpGet("{titleId}")]
        public IActionResult PopularActorInMovie(string titleId)
        {
            var titleEpisodes = _functionService.PopularActorsInMovieSearch(titleId);
            //return Ok(titleEpisodes);
            return Ok(titleEpisodes);
        }
        
    }
}
