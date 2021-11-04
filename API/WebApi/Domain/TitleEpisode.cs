namespace WebApi.Domain
{
    public class TitleEpisode
    {
        public string Id { get; set; }
        public string TitleId { get; set; }
        public int? SeasonNumber { get; set; }
        public int? EpisodeNumber { get; set; }
    }
}