using System;
using System.Linq;
using PhotoShare.Client.Core.Contracts;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    public class ModifyUserCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly ITownService townService;
        private readonly IUserSessionService userSessionService;

        public ModifyUserCommand(IUserService userService,ITownService townService,IUserSessionService userSessionService)
        {
            this.userService = userService;
            this.townService = townService;
            this.userSessionService = userSessionService;
        }

        // For example:
        // ModifyUser <username> <property> <new value>
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // Cannot change username!
        public string Execute(string[] data)
        {
            string username = data[0];
            string property = data[1];
            string value = data[2];

            if (!userSessionService.IsLoggedIn() || this.userSessionService.User.Username != username)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            bool userExists = this.userService.Exists(username);
            if (!userExists)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            int userId = this.userService.ByUsername<UserDto>(username).Id;
            if(property == "Password")
            {
                this.SetPassword(userId, value);
            }
            else if (property == "BornTown")
            {
                this.SetBornTown(userId, value);
            }
            else if(property == "CurrentTown")
            {
                this.SetCurrentTown(userId, value);
            }
            else
            {
                throw new ArgumentException($"Property {property} not supported!");
            }

            return $"User {username} {property} is {value}.";
        }

        private void SetPassword(int userId, string value)
        {
            bool isLower = value.Any(x => char.IsLower(x));
            bool isDigit = value.Any(x => char.IsDigit(x));
            if (!isLower || !isDigit)
            {
                throw new ArgumentException($"Invalid Password! Value {value} not valid!");
            }

            this.userService.ChangePassword(userId, value);
        }

        private void SetBornTown(int userId, string name)
        {
            bool townExists = townService.Exists(name);
            if (!townExists)
            {
                throw new ArgumentException($"Value {name} not valid.\nTown {name} not found!");
            }

            int townId = this.townService.ByName<TownDto>(name).Id;

            this.userService.SetBornTown(userId, townId);
        }

        private void SetCurrentTown(int userId, string name)
        {
            bool townExists = townService.Exists(name);
            if (!townExists)
            {
                throw new ArgumentException($"Value {name} not valid.\nTown {name} not found!");
            }

            int townId = this.townService.ByName<TownDto>(name).Id;

            this.userService.SetCurrentTown(userId, townId);
        }        
    }
}