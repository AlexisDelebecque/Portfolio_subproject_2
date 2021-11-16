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
        
        public int CountNameBasics()
        {
            return _ctx.NameBasics.Count();
        }
        
        // Get All NameBasic
        public IList<NameBasics> GetNameBasics(int page, int pageSize)
        {
            return _ctx.NameBasics.Skip(page * pageSize).Take(pageSize).ToList();
        }

        // Get One NameBasics
        public NameBasics GetNameBasic(int id)
        {
            return _ctx.NameBasics.Find(id);
        }
        #endregion

        #region OmdbDatas
        
        public int CountOmdbDatas()
        {
            return _ctx.OmdbDatas.Count();
        }
        
        // Get All OmdbData
        public IList<OmdbData> GetOmdbDatas(int page, int pageSize)
        {
            return _ctx.OmdbDatas.Skip(page * pageSize).Take(pageSize).ToList();
        }
        
        // Get One OmdbData
        public OmdbData GetOmdbData(int id)
        {
            return _ctx.OmdbDatas.Find(id);
        }
        #endregion

        #region TitleBasics
        
        public int CountTitleBasics()
        {
            return _ctx.TitleBasics.Count();
        }
        
        // Get All TitleBasics
        public IList<TitleBasics> GetTitleBasics(int page, int pageSize)
        {
            return _ctx.TitleBasics.Skip(page * pageSize).Take(pageSize).ToList();
        }
        
        // Get One TitleBasic
        public TitleBasics GetTitleBasic(int id)
        {
            return _ctx.TitleBasics.Find(id);
        }
        #endregion

        #region TitleAkas
        
        public int CountTitleAkas()
        {
            return _ctx.TitleAkas.Count();
        }
        
        // Get All TitleAkas
        public IList<TitleAkas> GetTitleAkas(int page, int pageSize)
        {
            return _ctx.TitleAkas.Skip(page * pageSize).Take(pageSize).ToList();
        }
        
        // Get One TitleBasic
        public TitleAkas GetTitleAka(int id)
        {
            return _ctx.TitleAkas.Find(id);
        }
        #endregion

        #region TitleCrew
        
        public int CountTitleCrews()
        {
            return _ctx.TitleCrews.Count();
        }
        
        // Get All TitleCrews
        public IList<TitleCrew> GetTitleCrews(int page, int pageSize)
        {
            return _ctx.TitleCrews.Skip(page * pageSize).Take(pageSize).ToList();
        }
        
        // Get One TitleCrew
        public TitleCrew GetTitleCrew(int id)
        {
            return _ctx.TitleCrews.Find(id);
        }
        #endregion

        #region TitleEpisodes
        
        public int CountTitleEpisodes()
        {
            return _ctx.TitleEpisodes.Count();
        }
        
        // Get All TitleEpisodes
        public IList<TitleEpisode> GetTitleEpisodes(int page, int pageSize)
        {
            return _ctx.TitleEpisodes.Skip(page * pageSize).Take(pageSize).ToList();
        }

        // Get One TitlePrincipals
        public TitleEpisode GetTitleEpisode(int id)
        {
            return _ctx.TitleEpisodes.Find(id);
        }
        #endregion

        #region TitlePrincipals
        
        public int CountTitlePrincipals()
        {
            return _ctx.TitlePrincipals.Count();
        }
        
        // Get All TitlePrincipals
        public IList<TitlePrincipals> GetTitlePrincipals(int page, int pageSize)
        {
            return _ctx.TitlePrincipals.Skip(page * pageSize).Take(pageSize).ToList();
        }

        // Get One TitlePrincipals
        public TitlePrincipals GetTitlePrincipal(int id)
        {
            return _ctx.TitlePrincipals.Find(id);
        }
        #endregion

        #region TitleRatings
        
        public int CountTitleRatings()
        {
            return _ctx.TitleRatings.Count();
        }
        
        // Get All TitleRating
        public IList<TitleRatings> GetTitleRatings(int page, int pageSize)
        {
            return _ctx.TitleRatings.Skip(page * pageSize).Take(pageSize).ToList();
        }

        // Get One TitleBasic
        public TitleRatings GetTitleRating(int id)
        {
            return _ctx.TitleRatings.Find(id);
        }
        #endregion

        #region Wi
        
        public int CountWis()
        {
            return _ctx.Wi.Count();
        }

        // Get All Wi
        public IList<Wi> GetWis(int page, int pageSize)
        {
            return _ctx.Wi.Skip(page * pageSize).Take(pageSize).ToList();
        }

        // Get One OmdbData
        public Wi GetWi(int id)
        {
            return _ctx.Wi.Find(id);
        }
        #endregion
    }
}