using System;
using System.Linq;
using TeamBuilder.App.Core.Commands;

namespace TeamBuilder.App.Core
{
    public class CommandDispatcher
    {
        private readonly AuthenticationManager authenticationManager;

        public CommandDispatcher(AuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;
        }

        public string Dispatch(string input)
        {
            string result = string.Empty;
            string[] inputArgs = input.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            string commandName = inputArgs.Length > 0 ? inputArgs[0] : string.Empty;
            string[] args = inputArgs.Skip(1).ToArray();
            switch (commandName)
            {
                case "RegisterUser":
                    RegisterUserCommand registerUser = new RegisterUserCommand(authenticationManager);
                    result = registerUser.Execute(args);
                    break;
                case "Login":
                    LoginCommand login = new LoginCommand(authenticationManager);
                    result = login.Execute(args);
                    break;
                case "Logout":
                    LoguotCommand loguot = new LoguotCommand(authenticationManager);
                    result = loguot.Execute(args);
                    break;
                case "DeleteUser":
                    DeleteUserCommand deleteUser = new DeleteUserCommand(authenticationManager);
                    result = deleteUser.Execute(args);
                    break;
                case "Exit":
                    ExitCommand exit = new ExitCommand();
                    result = exit.Execute(args);
                    break;
                default:
                    throw new NotSupportedException($"Command {commandName} not supported!");
            }

            return result;
        }
    }
}