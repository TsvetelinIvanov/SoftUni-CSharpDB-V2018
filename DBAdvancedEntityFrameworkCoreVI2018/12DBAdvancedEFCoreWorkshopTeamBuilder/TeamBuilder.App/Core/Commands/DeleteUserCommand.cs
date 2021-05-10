using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
    public class DeleteUserCommand : ICommand
    {
        private readonly AuthenticationManager authenticationManager;

        public DeleteUserCommand(AuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;
        }

        public string Execute(string[] args)
        {
            Check.CheckLength(0, args);
            this.authenticationManager.Authorize();
            User currentUser = this.authenticationManager.GetCurrentUser();
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                //string currentUserName = currentUser.FirstName + " " + currentUser.LastName ?? currentUser.LastName;
                //currentUser = context.Users.SingleOrDefault(u => u == currentUser);
                //if (currentUser == null)
                //{
                //    throw new ArgumentException($"User {currentUserName} is not available!");
                //}

                currentUser.IsDeleted = true;
                context.Users.Update(currentUser);
                context.SaveChanges();

                this.authenticationManager.Logout();

                return $"User {currentUser.Username} was deleted successfully!";
            }
        }
    }
}