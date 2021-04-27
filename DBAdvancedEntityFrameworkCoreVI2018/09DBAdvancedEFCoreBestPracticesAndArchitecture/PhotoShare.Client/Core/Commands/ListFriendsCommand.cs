using PhotoShare.Client.Core.Contracts;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoShare.Client.Core.Commands
{
    public class ListFriendsCommand : ICommand
    {
        private readonly IUserService userService;

        public ListFriendsCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(string[] args)
        {
            string username = args[0];

            bool userExists = this.userService.Exists(username);
            if (!userExists)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            UserFriendsDto user = this.userService.ByUsername<UserFriendsDto>(username);
            if (user.Friends.Count == 0)
            {
                return "No friends for this user. :(";
            }

            string result = this.ListUserFriends(user.Friends);

            return result;
        }

        private string ListUserFriends(ICollection<FriendDto> friends)
        {
            StringBuilder listedFriendsBuilder = new StringBuilder();
            listedFriendsBuilder.AppendLine("Friends:");
            foreach (FriendDto friend in friends.OrderBy(f => f.Username))
            {
                listedFriendsBuilder.AppendLine($"- {friend.Username}");
            }

            return listedFriendsBuilder.ToString().TrimEnd();
        }
    }
}