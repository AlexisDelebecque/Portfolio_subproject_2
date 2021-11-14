using WebApi.Services.UserServices;

namespace WebApiTests
{
    public static class UserUtils
    {
        public static void InitUser()
        {
            var service = new UserService();
            var user = service.GetUser("test");

            if (user != null)
            {
                service.DeleteUser(user.Username);
            }

            service.CreateUser("test", "1234", "key");
        }
        
        public static void DeleteUser()
        {
            var service = new UserService();
            var user = service.GetUser("test");

            if (user != null)
            {
                service.DeleteUser(user.Username);
            }
        }
    }
}