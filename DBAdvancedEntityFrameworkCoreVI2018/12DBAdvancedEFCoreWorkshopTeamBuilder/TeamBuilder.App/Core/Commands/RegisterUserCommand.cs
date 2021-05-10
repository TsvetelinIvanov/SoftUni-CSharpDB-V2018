using System;
using System.Linq;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;
using TeamBuilder.Models.Enums;

namespace TeamBuilder.App.Core.Commands
{
    public class RegisterUserCommand : ICommand
    {
        private readonly AuthenticationManager authenticationManager;

        public RegisterUserCommand(AuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;
        }

        public string Execute(string[] args)
        {
            Check.CheckLength(7, args);

            string username = args[0];
            if (username.Length < Constants.MinUsernameLength || username.Length > Constants.MaxUsernameLength)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.UsernameNotValid, username));
            }

            string password = args[1];
            bool isPasswordLegthValid = password.Length >= Constants.MinPasswordLength && password.Length <= Constants.MaxPasswordLength;
            bool isPasswordContentValid = password.Any(Char.IsDigit) && password.Any(Char.IsUpper);
            if (!isPasswordLegthValid || !isPasswordContentValid)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.PasswordNotValid, password));
            }

            string repeatedPassword = args[2];
            if (password != repeatedPassword)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.PasswordDoesNotMatch);
            }

            string firstName = args[3];
            if (firstName.Length > Constants.MaxFirstNameLength)
            {
                throw new ArgumentException($"The name must be no longer than {Constants.MaxFirstNameLength} characters!");
            }

            string lastName = args[4];
            if (firstName.Length > Constants.MaxLastNameLength)
            {
                throw new ArgumentException($"The name must be no longer than {Constants.MaxLastNameLength} characters!");
            }

            bool isNumber = int.TryParse(args[5], out int age);
            if (!isNumber || age < 0)
            {
                throw new ArgumentException(Constants.ErrorMessages.AgeNotValid);
            }

            bool isGenderValid = Enum.TryParse(args[6], out GenderEnum gender);
            if (!isGenderValid)
            {
                throw new ArgumentException(Constants.ErrorMessages.GenderNotValid);
            }

            if (CommandHelper.IsUserExicting(username))
            {
                throw new InvalidOperationException(string.Format(Constants.ErrorMessages.UsernameIsTaken, username));
            }

            if (authenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
            }

            this.RegisterUser(username, password, firstName, lastName, age, gender);

            return $"User {username} was registered successfully!";
        }

        private void RegisterUser(string username, string password, string firstName, string lastName, int age, GenderEnum gender)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                User user = new User()
                {
                    Username = username,
                    Password = password,
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age,
                    Gender = gender
                };

                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}