using System;
using System.Linq;
using PhotoShare.Client.Core.Contracts;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    public class AcceptFriendCommand : ICommand
    {
        private readonly IUserService userService;

        public AcceptFriendCommand(IUserService userService)
        {
            this.userService = userService;
        }
                
        public string Execute(string[] data)
        {
            string username = data[0];
            string friendUsername = data[1];
            if (username == null)
            {
                throw new ArgumentException($"{username} not found!");
            }

            if (friendUsername == null)
            {
                throw new ArgumentException($"{friendUsername} not found!");
            }

            UserFriendsDto user = this.userService.ByUsername<UserFriendsDto>(username);
            UserFriendsDto friend = this.userService.ByUsername<UserFriendsDto>(friendUsername);

            int userId = this.userService.ByUsername<UserFriendsDto>(username).Id;
            int friendId = this.userService.ByUsername<UserFriendsDto>(friendUsername).Id;

            if (this.userService.IfHaveFriendship(userId,friendId))
            {
                throw new InvalidOperationException($"{friendUsername} is already a friend to {username}");
            }

            if(user.Friends.Any(f => f.Username == friend.Username) && friend.Friends.Any(f => f.Username == user.Username))
            {
                throw new InvalidOperationException($"{friendUsername} is already a friend to {username}");
            }

            //UserFriendsDto user = this.userService.ByUsername<UserFriendsDto>(username);
            //UserFriendsDto friend = this.userService.ByUsername<UserFriendsDto>(friendUsername);

            //bool isAcceptedRequestFromUser = user.Friends.Any(x => x.Username == friend.Username);
            //bool isAcceptedRequestFromFriend = friend.Friends.Any(x => x.Username == user.Username);

            //if (!isAcceptedRequestFromUser)
            //{
            //    throw new InvalidOperationException($"{friendUsername} has not added {username} as a friend");
            //}
            //else if (isAcceptedRequestFromUser && !isAcceptedRequestFromFriend)
            //{
            //    throw new InvalidOperationException("Request is already sent!");
            //}
            //else if (isAcceptedRequestFromFriend && !isAcceptedRequestFromUser)
            //{
            //    throw new InvalidOperationException("Request is already sent!");
            //}

            this.userService.AcceptFriend(userId, friendId);

            return $"Friend {username} accepted {friendUsername} as a friend";
        }
    }
}