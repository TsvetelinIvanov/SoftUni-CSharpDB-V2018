using System;
using PhotoShare.Client.Core.Contracts;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Models.Enums;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    public class ShareAlbumCommand : ICommand
    {
        private readonly IAlbumService albumService;
        private readonly IUserService userService;
        private readonly IAlbumRoleService albumRoleService;
        private readonly IUserSessionService userSessionService;

        public ShareAlbumCommand(IAlbumService albumService,IUserService userService,IAlbumRoleService albumRoleService,IUserSessionService userSessionService)
        {
            this.albumService = albumService;
            this.userService = userService;
            this.albumRoleService = albumRoleService;
            this.userSessionService = userSessionService;
        }

        // For example:
        // ShareAlbum <albumId> <username> <permission>
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public string Execute(string[] data)
        {
            if (!userSessionService.IsLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            int albumId = int.Parse(data[0]);
            string username = data[1];
            string permission = data[2];

            bool userExists = this.userService.Exists(username);
            if (!userExists)
            {
                throw new ArgumentException($"User {username} not found!");
            }            
            
            AlbumDto album = this.albumService.ById<AlbumDto>(albumId);
            if (album == null)
            {
                throw new ArgumentException($"Album {albumId} not found!");
            }

            bool isValidRole = Enum.TryParse<Role>(permission, out Role result);
            if(!isValidRole)
            {
                throw new ArgumentException("Permission must be either “Owner” or “Viewer”!");
            }

            int userId = this.userService.ByUsername<UserFriendsDto>(username).Id;

            this.albumRoleService.PublishAlbumRole(albumId, userId, permission);

            return $"Username {username} added to album {album.Name} ({permission})";
        }
    }
}