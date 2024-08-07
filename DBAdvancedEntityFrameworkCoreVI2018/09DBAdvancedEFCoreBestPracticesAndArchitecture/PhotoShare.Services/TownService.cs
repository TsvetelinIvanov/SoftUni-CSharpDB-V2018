﻿using AutoMapper.QueryableExtensions;
using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoShare.Services
{
    public class TownService : ITownService
    {
        private readonly PhotoShareContext context;

        public TownService(PhotoShareContext context)
        {
            this.context = context;
        }

        public TModel ById<TModel>(int id) => this.By<TModel>(t => t.Id == id).SingleOrDefault();

        public TModel ByName<TModel>(string name) => this.By<TModel>(t => t.Name == name).SingleOrDefault();

        public bool Exists(int id) => this.ById<Town>(id) != null;

        public bool Exists(string name) => this.ByName<Town>(name) != null;

        public Town Add(string townName, string countryName)
        {
            Town town = new Town
            {
                Name = townName,
                Country = countryName
            };

            this.context.Towns.Add(town);
            this.context.SaveChanges();

            return town;
        }

        private IEnumerable<TModel> By<TModel>(Func<Town, bool> predicate) => this.context.Towns.Where(predicate).AsQueryable().ProjectTo<TModel>();
    }
}