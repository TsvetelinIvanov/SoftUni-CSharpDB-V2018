using System;
using PhotoShare.Client.Core.Contracts;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Client.Utilities;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    public class AddTagToCommand : ICommand
    {
        private readonly IAlbumService albumService;
        private readonly ITagService tagService;
        private readonly IAlbumTagService albumTagService;
        private readonly IUserSessionService userSessionService;

        public AddTagToCommand(IAlbumTagService albumTagService, IAlbumService albumService, ITagService tagService, IUserSessionService userSessionService)
        {
            this.albumTagService = albumTagService;
            this.tagService = tagService;
            this.albumService = albumService;
            this.userSessionService = userSessionService;
        }
        
        public string Execute(string[] args)
        {
            if (this.userSessionService.IsLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            string albumTitle = args[0];
            string tagName = args[1];
            tagName = tagName.ValidateOrTransform();            

            bool albumExists = this.albumService.Exists(albumTitle);
            bool tagExists = this.tagService.Exists(tagName);
            if (!albumExists || !tagExists)
            {
                throw new ArgumentException("Either tag or album do not exist!");
            }
            
            int albumId = this.albumService.ByName<AlbumDto>(albumTitle).Id;
            int tagId = this.tagService.ByName<TagDto>(tagName).Id;

            AlbumTag tagToAlbum = this.albumTagService.AddTagTo(albumId, tagId);

            return $"Tag {tagName} added to {albumTitle}!";
        }
    }
}