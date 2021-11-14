namespace WebApi.Domain.FunctionDomain
{
    public class SearchResultBestMatch
    {
        public char TitleId { get; set; }
        public int Rank { get; set; }
        public string PrimaryTitle { get; set; }
    }
}