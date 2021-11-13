using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.Domain;
using WebApi.Domain.UserDomain;
using WebApi.Domain.Movie;

namespace WebApi
{
    public class PortfolioContext: DbContext
    {
        public DbSet<TitleBasics> TitleBasics { get; set; }
        public DbSet<NameBasics> NameBasics { get; set; }
        public DbSet<OmdbData> OmdbDatas { get; set; }
        public DbSet<Wi> Wi { get; set; }
        public DbSet<TitleEpisode> TitleEpisodes { get; set; }
        public DbSet<TitleAkas> TitleAkas { get; set; }
        public DbSet<TitleCrew> TitleCrews { get; set; }
        public DbSet<TitlePrincipals> TitlePrincipals { get; set; }
        public DbSet<TitleRatings> TitleRatings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<SearchHistory> SearchHistories { get; set; }
        public DbSet<TitleBookmark> TitleBookmarks { get; set; }
        public DbSet<NameBookmark> NameBookmarks { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseNpgsql($"host={Environment.GetEnvironmentVariable("HOST")};" +
                                     $"db={Environment.GetEnvironmentVariable("DB")};" +
                                     $"uid={Environment.GetEnvironmentVariable("UID")};" +
                                     $"pwd={Environment.GetEnvironmentVariable("PWD")}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //movie schema
            modelBuilder.HasDefaultSchema("movie");

            // TitleBasics
            modelBuilder.Entity<TitleBasics>().ToTable("titlebasics");
            modelBuilder.Entity<TitleBasics>().Property(x => x.Id).HasColumnName("titleid");
            modelBuilder.Entity<TitleBasics>().Property(x => x.TitleType).HasColumnName("titletype");
            modelBuilder.Entity<TitleBasics>().Property(x => x.PrimaryTitle).HasColumnName("primarytitle");
            modelBuilder.Entity<TitleBasics>().Property(x => x.OriginalTitle).HasColumnName("originaltitle");
            modelBuilder.Entity<TitleBasics>().Property(x => x.IsAdult).HasColumnName("isadult");
            modelBuilder.Entity<TitleBasics>().Property(x => x.StartYear).HasColumnName("startyear");
            modelBuilder.Entity<TitleBasics>().Property(x => x.EndYear).HasColumnName("endyear");
            modelBuilder.Entity<TitleBasics>().Property(x => x.RuntimeMinutes).HasColumnName("runtimeminutes");
            modelBuilder.Entity<TitleBasics>().Property(x => x.Genres).HasColumnName("genres");

            // TitlePrincipals
            modelBuilder.Entity<TitlePrincipals>().ToTable("titleprincipals");
            modelBuilder.Entity<TitlePrincipals>().Property(x => x.Id).HasColumnName("titleid");
            modelBuilder.Entity<TitlePrincipals>().Property(x => x.Ordering).HasColumnName("ordering");
            modelBuilder.Entity<TitlePrincipals>().Property(x => x.NameId).HasColumnName("nameid");
            modelBuilder.Entity<TitlePrincipals>().Property(x => x.Category).HasColumnName("category");
            modelBuilder.Entity<TitlePrincipals>().Property(x => x.Job).HasColumnName("job");
            modelBuilder.Entity<TitlePrincipals>().Property(x => x.Characters).HasColumnName("characters");

            // TitleCrew
            modelBuilder.Entity<TitleCrew>().ToTable("titlecrew");
            modelBuilder.Entity<TitleCrew>().Property(x => x.Id).HasColumnName("titleid");
            modelBuilder.Entity<TitleCrew>().Property(x => x.Directors).HasColumnName("directors");
            modelBuilder.Entity<TitleCrew>().Property(x => x.Writers).HasColumnName("writers");

            // TitleAkas
            modelBuilder.Entity<TitleAkas>().ToTable("titleakas");
            modelBuilder.Entity<TitleAkas>().Property(x => x.Id).HasColumnName("titleid");
            modelBuilder.Entity<TitleAkas>().Property(x => x.Ordering).HasColumnName("ordering");
            modelBuilder.Entity<TitleAkas>().Property(x => x.Title).HasColumnName("title");
            modelBuilder.Entity<TitleAkas>().Property(x => x.Region).HasColumnName("region");
            modelBuilder.Entity<TitleAkas>().Property(x => x.Language).HasColumnName("language");
            modelBuilder.Entity<TitleAkas>().Property(x => x.Types).HasColumnName("types");
            modelBuilder.Entity<TitleAkas>().Property(x => x.Attributes).HasColumnName("attributes");
            modelBuilder.Entity<TitleAkas>().Property(x => x.IsOriginalTitle).HasColumnName("isoriginaltitle");

            // TitleEpisode
            modelBuilder.Entity<TitleEpisode>().ToTable("titleepisode");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.Id).HasColumnName("titleid");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.TitleId).HasColumnName("parenttid");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.SeasonNumber).HasColumnName("seasonnumber");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.EpisodeNumber).HasColumnName("episodenumber");

            // TitleRatings 
            modelBuilder.Entity<TitleRatings>().ToTable("titleratings");
            modelBuilder.Entity<TitleRatings>().Property(x => x.Id).HasColumnName("titleid");
            modelBuilder.Entity<TitleRatings>().Property(x => x.AverageRating).HasColumnName("averagerating");
            modelBuilder.Entity<TitleRatings>().Property(x => x.NumVotes).HasColumnName("numvotes");

            // NameBasics
            modelBuilder.Entity<NameBasics>().ToTable("namebasics");
            modelBuilder.Entity<NameBasics>().Property(x => x.Id).HasColumnName("nameid");
            modelBuilder.Entity<NameBasics>().Property(x => x.PrimaryName).HasColumnName("primaryname");
            modelBuilder.Entity<NameBasics>().Property(x => x.BirthYear).HasColumnName("birthyear");
            modelBuilder.Entity<NameBasics>().Property(x => x.DeathYear).HasColumnName("deathyear");
            modelBuilder.Entity<NameBasics>().Property(x => x.PrimaryProfession).HasColumnName("primaryprofession");
            modelBuilder.Entity<NameBasics>().Property(x => x.KnownForTitles).HasColumnName("knownfortitles");

            // Wi
            modelBuilder.Entity<Wi>().ToTable("wi");
            modelBuilder.Entity<Wi>().Property(x => x.Id).HasColumnName("titleid");
            modelBuilder.Entity<Wi>().Property(x => x.Word).HasColumnName("word");
            modelBuilder.Entity<Wi>().Property(x => x.Field).HasColumnName("field");
            modelBuilder.Entity<Wi>().Property(x => x.Lexeme).HasColumnName("lexeme");

            // OmdbDatas
            modelBuilder.Entity<OmdbData>().ToTable("omdb_data");
            modelBuilder.Entity<OmdbData>().Property(x => x.Id).HasColumnName("titleid");
            modelBuilder.Entity<OmdbData>().Property(x => x.Poster).HasColumnName("poster");
            modelBuilder.Entity<OmdbData>().Property(x => x.Awards).HasColumnName("awards");
            modelBuilder.Entity<OmdbData>().Property(x => x.Plot).HasColumnName("plot");



            //user schema
            modelBuilder.HasDefaultSchema("user");
            modelBuilder.Entity<User>().ToTable("user");
            modelBuilder.Entity<User>().HasKey(x => x.Username);
            modelBuilder.Entity<User>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<User>().Property(x => x.Password).HasColumnName("password");
            modelBuilder.Entity<User>().Property(x => x.Salt).HasColumnName("salt");
            modelBuilder.Entity<User>().Property(x => x.IsAdmin).HasColumnName("isadmin");
            modelBuilder.Entity<User>().Property(x => x.IsAdult).HasColumnName("isadult");
            
            modelBuilder.Entity<Rating>().ToTable("ratings");
            modelBuilder.Entity<Rating>().HasKey(x => new { x.Username, x.TitleId });
            modelBuilder.Entity<Rating>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<Rating>().Property(x => x.TitleId).HasColumnName("titleid");
            modelBuilder.Entity<Rating>().Property(x => x.Rate).HasColumnName("rate");
            modelBuilder.Entity<Rating>().Property(x => x.Comment).HasColumnName("comment");
            
            modelBuilder.Entity<SearchHistory>().ToTable("searchhistory");
            modelBuilder.Entity<SearchHistory>().HasKey(x => new { x.Username, x.SearchKey });
            modelBuilder.Entity<SearchHistory>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<SearchHistory>().Property(x => x.SearchKey).HasColumnName("searchkey");
            
            modelBuilder.Entity<TitleBookmark>().ToTable("titlebookmark");
            modelBuilder.Entity<TitleBookmark>().HasKey(x => new { x.Username, x.TitleId });
            modelBuilder.Entity<TitleBookmark>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<TitleBookmark>().Property(x => x.TitleId).HasColumnName("titleid");
            
            modelBuilder.Entity<NameBookmark>().ToTable("namebookmark");
            modelBuilder.Entity<NameBookmark>().HasKey(x => new { x.Username, x.NameId });
            modelBuilder.Entity<NameBookmark>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<NameBookmark>().Property(x => x.NameId).HasColumnName("nameid");

            
        }
    }
}
