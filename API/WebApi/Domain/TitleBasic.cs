using Microsoft.Net.Http.Headers;

namespace WebApi.Domain
{
    public class TitleBasic
    {
        public char TitleId { get; set; }

        public string TitleType { get; set; }

        public string PrimaryTitle { get; set; }

        public string OriginalTitle { get; set; }

        public bool IsAdult { get; set; } //boolean, fordi true/false?

        public char StartYear { get; set; }

        public char EndYear { get; set; }

        public int RunTimeMinutes { get; set; }

        public string Genres { get; set; }

    }
}