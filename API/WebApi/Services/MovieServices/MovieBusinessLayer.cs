using System.Collections.Generic;
using System.Linq;
using WebApi.Domain;
using WebApi.Domain.MovieDomain;

namespace WebApi.Services.MovieServices
{
    public class MovieBusinessLayer
    {
        private readonly PortfolioContext _ctx = new();

        #region NameBasics
        public IList<NameBasics> GetNameBasics()
        {
            return _ctx.NameBasics.ToList();
        }

        // Get One NameBasics
        public NameBasics GetNameBasic(int id)
        {
            return _ctx.NameBasics.Find(id);
        }
        #endregion

        #region OmdbDatas
        // Get All OmdbData
        public IList<OmdbData> GetOmdbDatas()
        {
            return _ctx.OmdbDatas.ToList();
        }

        // Get One OmdbData
        public OmdbData GetOmdbData(int id)
        {
            return _ctx.OmdbDatas.Find(id);
        }
        #endregion

        #region TitleBasics
        // Get All TitleBasics
        public IList<TitleBasics> GetTitleBasics()
        {
            return _ctx.TitleBasics.ToList();
        }

        // Get One TitleBasic
        public TitleBasics GetTitleBasic(int id)
        {
            return _ctx.TitleBasics.Find(id);
        }
        #endregion

        #region TitleAkas
        // Get All TitleBasics
        public IList<TitleAkas> GetTitleAkas()
        {
            return _ctx.TitleAkas.ToList();
        }

        // Get One TitleBasic
        public TitleAkas GetTitleAka(int id)
        {
            return _ctx.TitleAkas.Find(id);
        }
        #endregion

        #region TitleCrew
        // Get All TitleCrew
        public IList<TitleCrew> GetTitleCrews()
        {
            return _ctx.TitleCrews.ToList();
        }

        // Get One TitleCrew
        public TitleCrew GetTitleCrew(int id)
        {
            return _ctx.TitleCrews.Find(id);
        }
        #endregion

        #region TitleEpisodes
        // Get All TitleEpisodes

        public IList<TitleEpisode> GetTitleEpisodes()
        {
            return _ctx.TitleEpisodes.ToList();
        }

        // Get One TitlePrincipals
        public TitleEpisode GetTitleEpisode(int id)
        {
            return _ctx.TitleEpisodes.Find(id);
        }
        #endregion

        #region TitlePrincipals
        // Get All TitlePrincipals
        public IList<TitlePrincipals> GetTitlePrincipals()
        {
            return _ctx.TitlePrincipals.ToList();
        }

        // Get One TitlePrincipals
        public TitlePrincipals GetTitlePrincipal(int id)
        {
            return _ctx.TitlePrincipals.Find(id);
        }
        #endregion

        #region TitleRatings
        // Get All TitleRating
        public IList<TitleRatings> GetTitleRatings()
        {
            return _ctx.TitleRatings.ToList();
        }

        // Get One TitleBasic
        public TitleRatings GetTitleRating(int id)
        {
            return _ctx.TitleRatings.Find(id);
        }
        #endregion

        #region Wi

        // Get All Wi
        public IList<Wi> GetWis()
        {
            return _ctx.Wi.ToList();
        }

        // Get One OmdbData
        public Wi GetWi(int id)
        {
            return _ctx.Wi.Find(id);
        }
        #endregion
    }
}