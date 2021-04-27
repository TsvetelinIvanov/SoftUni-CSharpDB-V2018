using System;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Client.Core.Contracts;
using PhotoShare.Services.Contracts;
using PhotoShare.Models;

namespace PhotoShare.Client.Core.Commands
{
    public class UploadPictureCommand : ICommand
    {
        private readonly IPictureService pictureService;
        private readonly IAlbumService albumService;
        private readonly IUserSessionService userSessionService;

        public UploadPictureCommand(IPictureService pictureService, IAlbumService albumService,IUserSessionService userSessionService)
        {
            this.pictureService = pictureService;
            this.albumService = albumService;
            this.userSessionService = userSessionService;
        }
                
        public string Execute(string[] data)
        {
            if (!userSessionService.IsLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            string albumName = data[0];
            string pictureTitle = data[1];
            string path = data[2];            

            bool albumExists = this.albumService.Exists(albumName);
            if (!albumExists)
            {
                throw new ArgumentException($"Album {albumName} not found!");
            }

            int albumId = this.albumService.ByName<AlbumDto>(albumName).Id;

            Picture picture = this.pictureService.Create(albumId, pictureTitle, path);

            return $"Picture {pictureTitle} added to {albumName}!";
        }
    }
}