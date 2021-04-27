using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PhotoShare.Client.Core.Contracts;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    public class RegisterUserCommand : ICommand
    {
        private readonly IUserService userService;

        public RegisterUserCommand(IUserService userService)
        {
            this.userService = userService;
        }
                
        public string Execute(string[] data)
        {
            if (data.Length != 4)
            {
                return "Command RegisterUser not vaid!";
            }

            string username = data[0];
            string password = data[1];
            string repeatPassword = data[2];
            string email = data[3];

            RegisterUserDto registerUserDto = new RegisterUserDto
            {
                Username = username,
                Password = password,
                Email = email
            };

            if(!this.IsValid(registerUserDto))
            {
                throw new ArgumentException("Invalid data!");
            }

            bool userExists = this.userService.Exists(username);
            if(userExists)
            {
                throw new InvalidOperationException($"Username {username} is already taken!");
            }

            if(password != repeatPassword)
            {
                throw new ArgumentException("Passwords do not match!");
            }

            this.userService.Register(username, password, email);

            return $"User {username} was registered successfully!";

        }

        private bool IsValid(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}