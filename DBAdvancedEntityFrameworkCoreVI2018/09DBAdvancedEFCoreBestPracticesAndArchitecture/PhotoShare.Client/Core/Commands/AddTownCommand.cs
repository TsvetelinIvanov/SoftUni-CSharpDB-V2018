using System;
using PhotoShare.Client.Core.Contracts;
using PhotoShare.Services.Contracts;
using PhotoShare.Models;

namespace PhotoShare.Client.Core.Commands
{
    public class AddTownCommand : ICommand
    {
        private readonly ITownService townService;
        private readonly IUserSessionService userSessionService;

        public AddTownCommand(ITownService townService,IUserSessionService userSessionService)
        {
            this.townService = townService;
            this.userSessionService = userSessionService;
        }
                
        public string Execute(string[] data)
        {
            if (!userSessionService.IsLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            string townName = data[0];
            string country = data[1];
            bool townExists = this.townService.Exists(townName);
            if (townExists)
            {
                throw new ArgumentException($"Town {townName} was already added!");
            }

            Town town = this.townService.Add(townName, country);

            return $"Town {townName} was added successfully!";
        }
    }
}