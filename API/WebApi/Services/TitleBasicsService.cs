using System.Collections.Generic;
using System.Linq;
using WebApi.Domain;

namespace WebApi.Services
{
    public class TitleBasicsService
    {
        private readonly PortfolioContext _ctx;

        public TitleBasicsService()
        {
            _ctx = new PortfolioContext();
        }


        public IList<TitleBasic> GetTitleBasics()
        {
            return _ctx.TitleBasics.ToList(); //burde vi ikke returne objekt her? Vi returner liste af objekter. 
        }

    }
}