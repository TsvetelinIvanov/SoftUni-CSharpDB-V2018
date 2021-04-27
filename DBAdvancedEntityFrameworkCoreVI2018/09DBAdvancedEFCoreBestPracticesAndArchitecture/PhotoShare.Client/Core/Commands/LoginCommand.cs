using PhotoShare.Client.Core.Contracts;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;
using System;

namespace PhotoShare.Client.Core.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly IUserSessionService userSessionService;

        public LoginCommand(IUserSessionService userSessionService)
        {
            this.userSessionService = userSessionService;
        }

        public string Execute(string[] args)
        {
            if (this.userSessionService.IsLoggedIn())
            {
                throw new ArgumentException($"You should logout first!");
            }

            string username = args[0];
            string password = args[1];
            User user = this.userSessionService.Login(username, password);
            if(user == null)
            {
                throw new ArgumentException("Invalid username or password!");
            }

            return $"User {user.Username} successfully logged in!";
        }
    }
}