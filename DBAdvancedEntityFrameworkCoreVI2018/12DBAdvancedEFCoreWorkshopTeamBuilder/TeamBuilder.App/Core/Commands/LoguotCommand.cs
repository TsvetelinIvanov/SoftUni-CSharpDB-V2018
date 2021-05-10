using TeamBuilder.App.Utilities;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
    public class LoguotCommand : ICommand
    {
        private readonly AuthenticationManager authenticationManager;

        public LoguotCommand(AuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;
        }

        public string Execute(string[] args)
        {
            Check.CheckLength(0, args);
            this.authenticationManager.Authorize();
            User currentUser = this.authenticationManager.GetCurrentUser();
            this.authenticationManager.Logout();

            return $"User {currentUser.Username} successfully logged out!";
        }
    }
}