using System.Collections.Generic;

namespace PhotoShare.Client.Core.Dtos
{
    public class UserFriendsDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public ICollection<FriendDto> Friends { get; set; }
    }
}