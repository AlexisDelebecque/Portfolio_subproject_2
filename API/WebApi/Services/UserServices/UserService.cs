using System;
using System.Linq;
using WebApi.Domain.UserDomain;

namespace WebApi.Services.UserServices
{
    public class UserService
    {
        private readonly PortfolioContext _ctx;

        public UserService()
        {
            _ctx = new PortfolioContext();
        }
        
        public User GetUser(string username)
        {
            return _ctx.Users.FirstOrDefault(x => x.Username == username);
        }

        public User GetUserByCredentials(string username, string password)
        {
            return _ctx.Users.FirstOrDefault(x => x.Username == username && x.Password == password);
        }
        
        public User CreateUser(string username, string password, string salt)
        {
            if (GetUserByCredentials(username, password) != null)
                return null;
            
            var user = new User()
            {
                Username = username,
                Password = password,
                Salt = salt,
                IsAdmin = false,
                IsAdult = false,
            };
            
            _ctx.Users.Add(user);
            _ctx.SaveChanges();
            return user;
        }

        public bool UpdateUserIsAdmin(string username, bool isAdmin)
        {
            var user = _ctx.Users.FirstOrDefault(x => x.Username == username);

            if (user == null)
                return false;
            user.IsAdmin = isAdmin;
            _ctx.SaveChanges();
            return true;
        }

        public bool DeleteUser(string username)
        {
            var userToRemove = GetUser(username);

            if (userToRemove == null)
                return false;
            
            _ctx.Users.Remove(userToRemove);
            return _ctx.SaveChanges() > 0;   
        }

    }
}