using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Services
{
    public class UserSessionService : IUserSessionService
    {
        private readonly IUserService userService;       

        public UserSessionService(IUserService userService)
        {
            this.userService = userService;
        }
        
        public User User { get; private set; }

        public bool IsLoggedIn() => this.User != null;

        public User Login (string username,string password)
        {
            this.User = userService.ByUsernameAndPassword<User>(username, password);

            return this.User;
        }

        public void Logout() => this.User = null;       
    }
}