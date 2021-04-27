using System;
using PhotoShare.Client.Core.Contracts;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    public class AddFriendCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly IUserSessionService userSessionService;

        public AddFriendCommand(IUserService userService,IUserSessionService userSessionService)
        {
            this.userService = userService;
            this.userSessionService = userSessionService;
        }
        
        public string Execute(string[] data)
        {
            string username = data[0];
            string friendUsername = data[1];
            if (!userSessionService.IsLoggedIn() || userSessionService.User.Username != username)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            UserFriendsDto user = this.userService.ByUsername<UserFriendsDto>(username);
            UserFriendsDto friend = this.userService.ByUsername<UserFriendsDto>(friendUsername);
            if(user == null)
            {
                throw new ArgumentException($"User {username} not found!");
            }
            if(friend == null)
            { 
            throw new ArgumentException($"User {friendUsername} not found!");
            }

           if(this.userService.IfHaveFriendship(user.Id,friend.Id) || this.userService.IfHaveFriendship(friend.Id, user.Id))
            { 
                throw new InvalidOperationException($"{friend.Username} is already a friend to {user.Username}");
            }
            

            int userId = this.userService.ByUsername<UserDto>(username).Id;
            int friendId = this.userService.ByUsername<UserDto>(friendUsername).Id;

            this.userService.AddFriend(userId, friendId);

            return $"Friend {friend.Username} added to {user.Username}";
        }
    }
}