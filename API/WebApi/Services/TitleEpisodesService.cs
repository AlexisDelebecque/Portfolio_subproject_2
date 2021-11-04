using System.Collections.Generic;
using System.Linq;
using WebApi.Domain;

namespace WebApi.Services
{
    public class TitleEpisodesService
    {
        private readonly PortfolioContext _ctx;

        public TitleEpisodesService()
        {
            _ctx = new PortfolioContext();
        }


        public IList<TitleEpisode> GetTitleEpisodes()
        {
            return _ctx.TitleEpisodes.ToList(); //burde vi ikke returne objekt her? Vi returner liste af objekter. 
        }

        //ny metode, hvor vi henter en anden ting to list. Eller lave service til hver ting. 

    }
}