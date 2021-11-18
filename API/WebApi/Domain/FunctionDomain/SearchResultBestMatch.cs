namespace WebApi.Domain.FunctionDomain
{
    public class SearchResultBestMatch
    {
        public string TitleId { get; set; }
        public int Rank { get; set; }
        public string PrimaryTitle { get; set; }
    }
}