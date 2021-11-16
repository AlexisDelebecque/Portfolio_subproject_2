using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.Domain;
using WebApi.Domain.FunctionDomain;
using WebApi.Domain.UserDomain;

namespace WebApi
{
    public class PortfolioContext: DbContext
    {
        public DbSet<TitleEpisode> TitleEpisodes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<SearchHistory> SearchHistories { get; set; }
        public DbSet<TitleBookmark> TitleBookmarks { get; set; }
        public DbSet<NameBookmark> NameBookmarks { get; set; }

        //added for methods
        public DbSet<SearchResultExactMatch> SearchResultsExactMatches { get; set; }
        public DbSet<SearchResultBestMatch> SearchResultsBestMatches { get; set; }
        public DbSet<SearchResultsPopularActorsInMovie> SearchResultsPopularActorsInMovies { get; set; }
        public DbSet<SearchResultsCoPlayers> SearchResultsCoPlayers { get; set; }
        public DbSet<SearchResultStructuredStringSearch> SearchResultStructuredStringSearches { get; set; }
        public DbSet<SearchResultStructuredActorSearch> SearchResultStructuredActorSearches { get; set; }

        public DbSet<SearchResultFindRating> SearchResultFindRatings { get; set; }

        public DbSet<SearchResultRecommended> SearchResultRecommendeds { get; set; }

        public DbSet<SearchResultsPopularActorsCoPlayers> SearchResultsPopularActorsCoPlayers { get; set; }

        private readonly string _connectionString;

        public PortfolioContext(string connectionString = "")
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                _connectionString = $"host={Environment.GetEnvironmentVariable("HOST")};" +
                                    $"db={Environment.GetEnvironmentVariable("DB")};" +
                                    $"uid={Environment.GetEnvironmentVariable("UID")};" +
                                    $"pwd={Environment.GetEnvironmentVariable("PWD")}";
            } else 
                _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //movie schema
            modelBuilder.HasDefaultSchema("movie");
            modelBuilder.Entity<TitleEpisode>().ToTable("titleepisode");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.Id).HasColumnName("titleid");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.TitleId).HasColumnName("parenttid");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.SeasonNumber).HasColumnName("seasonnumber");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.EpisodeNumber).HasColumnName("episodenumber");
            
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

            //adding for methods: 

            modelBuilder.Entity<SearchResultExactMatch>().HasNoKey();
            modelBuilder.Entity<SearchResultExactMatch>().Property(x => x.TitleId).HasColumnName("titleid");
            modelBuilder.Entity<SearchResultExactMatch>().Property(x => x.PrimaryTitle).HasColumnName("primarytitle");

            modelBuilder.Entity<SearchResultBestMatch>().HasNoKey();
            modelBuilder.Entity<SearchResultBestMatch>().Property(x => x.TitleId).HasColumnName("titleid");
            modelBuilder.Entity<SearchResultBestMatch>().Property(x => x.Rank).HasColumnName("rank");
            modelBuilder.Entity<SearchResultBestMatch>().Property(x => x.PrimaryTitle).HasColumnName("primarytitle");

            modelBuilder.Entity<SearchResultsPopularActorsInMovie>().HasNoKey();
            modelBuilder.Entity<SearchResultsPopularActorsInMovie>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<SearchResultsPopularActorsInMovie>().Property(x => x.Primaryname).HasColumnName("primaryname");
            modelBuilder.Entity<SearchResultsPopularActorsInMovie>().Property(x => x.Rating).HasColumnName("rating");

            modelBuilder.Entity<SearchResultsCoPlayers>().HasNoKey();
            modelBuilder.Entity<SearchResultsCoPlayers>().Property(x => x.CoPlayerId).HasColumnName("co_playerid");
            modelBuilder.Entity<SearchResultsCoPlayers>().Property(x => x.PrimaryName).HasColumnName("primaryname");
            modelBuilder.Entity<SearchResultsCoPlayers>().Property(x => x.Frequency).HasColumnName("frequency");

            modelBuilder.Entity<SearchResultStructuredStringSearch>().HasNoKey();
            modelBuilder.Entity<SearchResultStructuredStringSearch>().Property(x => x.TitleId).HasColumnName("titleid");
            modelBuilder.Entity<SearchResultStructuredStringSearch>().Property(x => x.PrimaryTitle).HasColumnName("primarytitle");

            modelBuilder.Entity<SearchResultStructuredActorSearch>().HasNoKey();
            modelBuilder.Entity<SearchResultStructuredActorSearch>().Property(x => x.NameId).HasColumnName("nameid");
            modelBuilder.Entity<SearchResultStructuredActorSearch>().Property(x => x.PrimaryName).HasColumnName("primaryname");

            modelBuilder.Entity<SearchResultFindRating>().HasNoKey();
            modelBuilder.Entity<SearchResultFindRating>().Property(x => x.Rating).HasColumnName("res"); //Need to map to table instead

            modelBuilder.Entity<SearchResultRecommended>().HasNoKey();
            modelBuilder.Entity<SearchResultRecommended>().Property(x => x.PrimaryTitle).HasColumnName("primarytitle");


            modelBuilder.Entity<SearchResultsPopularActorsCoPlayers>().HasNoKey();
            modelBuilder.Entity<SearchResultsPopularActorsCoPlayers>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<SearchResultsPopularActorsCoPlayers>().Property(x => x.PrimaryName).HasColumnName("primaryname");
            modelBuilder.Entity<SearchResultsPopularActorsCoPlayers>().Property(x => x.Rating).HasColumnName("rating");



        }
    }
}
