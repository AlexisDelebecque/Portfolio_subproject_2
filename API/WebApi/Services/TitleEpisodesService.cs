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
            return _ctx.TitleEpisodes.ToList();
        }

    }
}