using AutoMapper.QueryableExtensions;
using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Models.Enums;
using PhotoShare.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoShare.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly PhotoShareContext context;

        public AlbumService(PhotoShareContext context)
        {
            this.context = context;
        }

        public TModel ById<TModel>(int id) => this.By<TModel>(a => a.Id == id).SingleOrDefault();

        public TModel ByName<TModel>(string name) => this.By<TModel>(a => a.Name == name).SingleOrDefault();

        public bool Exists(int id) => this.ById<Album>(id) != null;

        public bool Exists(string name) => this.ByName<Album>(name) != null;

        public Album Create(int userId, string albumTitle, string bgColor, string[] tags)
        {
            Color backgroundColor = Enum.Parse<Color>(bgColor,true);

            Album album = new Album
            {
                Name = albumTitle,
                BackgroundColor= backgroundColor,
            };

            this.context.Add(album);
            this.context.SaveChanges();

            AlbumRole albumRole = new AlbumRole
            {
                UserId = userId,
                Album = album
            };

            this.context.Add(albumRole);
            this.context.SaveChanges();

            foreach (string tag in tags)
            {
                int currentTagId = this.context.Tags.FirstOrDefault(t => t.Name == tag).Id;

                AlbumTag albumTag = new AlbumTag
                {
                    Album = album,
                    TagId = currentTagId
                };

                this.context.Add(albumTag);
            }

            this.context.SaveChanges();

            return album;
        }

        private IEnumerable<TModel> By<TModel>(Func<Album, bool> predicate) => this.context.Albums.Where(predicate).AsQueryable().ProjectTo<TModel>();
    }
}