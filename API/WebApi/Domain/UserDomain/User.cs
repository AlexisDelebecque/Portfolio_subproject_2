namespace WebApi.Domain.UserDomain
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsAdult { get; set; }
    }
}