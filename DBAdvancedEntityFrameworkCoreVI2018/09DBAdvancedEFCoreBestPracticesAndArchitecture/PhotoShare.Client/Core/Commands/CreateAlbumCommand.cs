﻿using System;
using System.Linq;
using PhotoShare.Client.Core.Contracts;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Client.Utilities;
using PhotoShare.Models.Enums;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    public class CreateAlbumCommand : ICommand
    {
        private readonly IAlbumService albumService;
        private readonly IUserService userService;
        private readonly ITagService tagService;
        private readonly IUserSessionService userSessionService;

        public CreateAlbumCommand(IAlbumService albumService, IUserService userService, ITagService tagService, IUserSessionService userSessionService)
        {
            this.albumService = albumService;
            this.userService = userService;
            this.tagService = tagService;
            this.userSessionService = userSessionService;
        }
        
        public string Execute(string[] data)
        {
            string username = data[0];
            string albumTitle = data[1];
            if (!userSessionService.IsLoggedIn() || this.userSessionService.User.Username != username)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }            

            bool userExists = this.userService.Exists(username);
            if(!userExists)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            bool albumExists = this.albumService.Exists(albumTitle);
            if (albumExists)
            {
                throw new ArgumentException($"Album {albumTitle} exists!");
            }

            string color = data[2];
            string[] tags = data.Skip(3).ToArray();            

            bool isValidColor = Enum.TryParse<Color>(color, out Color result);
            if(!isValidColor)
            {
                throw new ArgumentException($"Color {color} not found!");
            }

            for (int i = 0; i < tags.Length; i++)
            {
                tags[i] = tags[i].ValidateOrTransform();
            
                bool currentTag = this.tagService.Exists(tags[i]);
                if(!currentTag)
                {
                    throw new ArgumentException($"Invalid tags!");
                }
            }

            int userId = this.userService.ByUsername<UserDto>(username).Id;

            this.albumService.Create(userId, albumTitle, color, tags);

            return $"Album {albumTitle} successfully created!";
        }
    }
}
