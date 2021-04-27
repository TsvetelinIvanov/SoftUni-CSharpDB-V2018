using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;
using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Services
{
    public class PictureService : IPictureService
    {
        private readonly PhotoShareContext context;

        public PictureService(PhotoShareContext context)
        {
            this.context = context;
        }

        public TModel ById<TModel>(int id) => this.By<TModel>(p => p.Id == id).SingleOrDefault();

        public TModel ByTitle<TModel>(string name) => this.By<TModel>(p => p.Title == name).SingleOrDefault();

        public bool Exists(int id) => this.ById<Picture>(id) != null;

        public bool Exists(string name) => this.ByTitle<Picture>(name) != null;        

        public Picture Create(int albumId, string pictureTitle, string pictureFilePath)
        {
            Picture picture = new Picture()
            {
                Title = pictureTitle,
                Path = pictureFilePath,
                AlbumId = albumId
            };

            this.context.Pictures.Add(picture);
            this.context.SaveChanges();

            return picture;
        }

        private IEnumerable<TModel> By<TModel>(Func<Picture, bool> predicate) => this.context.Pictures.Where(predicate).AsQueryable().ProjectTo<TModel>();
    }
}