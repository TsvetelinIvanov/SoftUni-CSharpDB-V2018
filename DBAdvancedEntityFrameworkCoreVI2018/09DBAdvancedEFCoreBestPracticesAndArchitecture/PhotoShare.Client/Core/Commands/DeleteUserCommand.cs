using System;
using PhotoShare.Client.Core.Contracts;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    public class DeleteUserCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly IUserSessionService userSessionService;

        public DeleteUserCommand(IUserService userService,IUserSessionService userSessionService)
        {
            this.userService = userService;
            this.userSessionService = userSessionService;
        }
        
        public string Execute(string[] data)
        {
            string username = data[0];
            if (!userSessionService.IsLoggedIn() || this.userSessionService.User.Username != username)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            bool userExists = this.userService.Exists(username);
            if (!userExists)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            bool userIsDeleted = this.userService.IsDeleted(username);
            if (userIsDeleted)
            {
                throw new InvalidOperationException($"User {username} is already deleted!");
            }
            
            this.userService.Delete(username);

            return $"User {username} was deleted succesfully!";
        }
    }
}
