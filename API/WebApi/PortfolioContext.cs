using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.Domain;


namespace WebApi
{
    public class PortfolioContext: DbContext
    {
        public DbSet<TitleEpisode> TitleEpisodes { get; set; }
        //ny list med nogle andre ting. Lave klasse ud fra det
        public DbSet<TitleBasic> TitleBasics { get; set; }

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
            modelBuilder.Entity<TitleEpisode>().Property(x => x.Id).HasColumnName("titleid"); //Burde den ikke hedde titleId, for at de kan finde ud af med forskellige keys?
            modelBuilder.Entity<TitleEpisode>().Property(x => x.TitleId).HasColumnName("parenttid");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.SeasonNumber).HasColumnName("seasonnumber");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.EpisodeNumber).HasColumnName("episodenumber");

            modelBuilder.Entity<TitleBasic>().ToTable("titlebasics");
            modelBuilder.Entity<TitleBasic>().HasKey(x => x.TitleId);
            modelBuilder.Entity<TitleBasic>().Property(x => x.TitleId).HasColumnName("titleid");
            modelBuilder.Entity<TitleBasic>().Property(x => x.TitleType).HasColumnName("titletype");
            modelBuilder.Entity<TitleBasic>().Property(x => x.PrimaryTitle).HasColumnName("primarytitle");
            modelBuilder.Entity<TitleBasic>().Property(x => x.OriginalTitle).HasColumnName("originaltitle");
            modelBuilder.Entity<TitleBasic>().Property(x => x.IsAdult).HasColumnName("isadult");
            modelBuilder.Entity<TitleBasic>().Property(x => x.StartYear).HasColumnName("startyear");
            modelBuilder.Entity<TitleBasic>().Property(x => x.EndYear).HasColumnName("endyear");
            modelBuilder.Entity<TitleBasic>().Property(x => x.RunTimeMinutes).HasColumnName("runtimeminutes");
            modelBuilder.Entity<TitleBasic>().Property(x => x.Genres).HasColumnName("genres");


        }
    }
}
