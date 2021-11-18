using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.FunctionDomain;
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

        //[HttpGet("bestmatch")]


        //public IActionResult BestMatchSearch([FromRoute]string[] strings) //Cant figure out the multiple inputs
        [HttpGet("{strings}")]
        public IActionResult BestMatchSearch(string strings)
        {
            
            string[] searchStrings = strings.Split(',');
            var bestMatches = _functionService.BestMatchSearch(searchStrings);
            return Ok(bestMatches); //Forstår ikke hvorfor det her ikke fungerer. Noget med typerne. 
            //return Ok(searchStrings);
        }
        /*
        
        [HttpGet("{titleId}")]
        public IActionResult PopularActorInMovie(string titleId)
        {
            var titleEpisodes = _functionService.PopularActorsInMovieSearch(titleId);
            //return Ok(titleEpisodes);
            return Ok(titleEpisodes);
        }
        */
    }
        
}
