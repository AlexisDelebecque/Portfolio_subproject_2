using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.Domain;

namespace WebApi
{
    public class PortfolioContext: DbContext
    {
        public DbSet<TitleEpisode> TitleEpisodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            optionsBuilder.UseNpgsql($"host={Environment.GetEnvironmentVariable("HOST")};" +
                                     $"db={Environment.GetEnvironmentVariable("DB")};" +
                                     $"uid={Environment.GetEnvironmentVariable("UID")};" +
                                     $"pwd={Environment.GetEnvironmentVariable("PWD")}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("movie");
            modelBuilder.Entity<TitleEpisode>().ToTable("titleepisode");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.Id).HasColumnName("titleid");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.TitleId).HasColumnName("parenttid");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.SeasonNumber).HasColumnName("seasonnumber");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.EpisodeNumber).HasColumnName("episodenumber");
        }
    }
}
