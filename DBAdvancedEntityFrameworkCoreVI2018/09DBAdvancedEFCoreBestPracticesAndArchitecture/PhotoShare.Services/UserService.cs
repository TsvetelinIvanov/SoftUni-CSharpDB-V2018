using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoShare.Services
{
    public class UserService : IUserService
    {
        private readonly PhotoShareContext context;

        public UserService(PhotoShareContext context)
        {
            this.context = context;
        }

        public TModel ById<TModel>(int id) => this.By<TModel>(u => u.Id == id).SingleOrDefault();

        public TModel ByUsername<TModel>(string username) => this.By<TModel>(u => u.Username == username).SingleOrDefault();   

        public bool Exists(int id) => this.ById<User>(id) != null;

        public bool Exists(string name) => this.ByUsername<User>(name) != null;

        public User Register(string username, string password, string email)
        {
            User user = new User
            {
                Username = username,
                Password = password,
                Email = email,
                IsDeleted = false
            };

            this.context.Users.Add(user);
            this.context.SaveChanges();

            return user;
        }

        public void ChangePassword(int userId, string password)
        {
            User user = this.ById<User>(userId);
            user.Password = password;
            this.context.SaveChanges();
        }       

        public bool CheckPassword(string username, string password)
        {
            bool exists = this.UserExists(username);

            return this.context.Users.FirstOrDefault(u => u.Username == username).Password == password;
        }

        public bool UserExists(string username)
        {
            return this.context.Users.Any(u => u.Username == username);
        }

        public void SetBornTown(int userId, int townId)
        {
            User user = this.ById<User>(userId);
            user.BornTownId = townId;
            this.context.SaveChanges();
        }

        public void SetCurrentTown(int userId, int townId)
        {
            User user = this.ById<User>(userId);
            user.CurrentTownId = townId;
            this.context.SaveChanges();
        }       

        public Friendship AddFriend(int userId, int friendId)
        {
            this.FriendsReverseSide(userId, friendId);

            Friendship friendship = new Friendship
            {
                UserId = userId,
                FriendId = friendId
            };

            this.context.Friendships.Add(friendship);
            this.context.SaveChanges();

            return friendship;
        }

        public Friendship AcceptFriend(int userId, int friendId)
        {
            Friendship friendship = new Friendship
            {
                UserId = userId,
                FriendId = friendId
            };

            this.context.Friendships.Add(friendship);
            this.context.SaveChanges();

            return friendship;
        }

        public bool IfHaveFriendship(int userId,int friendId)
        {
            return this.context.Friendships.Any(f => f.UserId == userId && f.FriendId == friendId);
        }        

        public void Delete(string username)
        {
            User user = context.Users.FirstOrDefault(u => u.Username == username);
            user.IsDeleted = true;
            this.context.SaveChanges();
        }

        public bool IsDeleted(string username)
        {
            User user = this.ByUsername<User>(username);
            if (user.IsDeleted == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public TModel ByUsernameAndPassword<TModel>(string username, string password) => this.By<TModel>(u => u.Username == username && u.Password == password).SingleOrDefault();

        private void FriendsReverseSide(int userId, int friendId)
        {
            Friendship friendship = new Friendship
            {
                UserId = friendId,
                FriendId = userId
            };

            this.context.Friendships.Add(friendship);
            context.SaveChanges();
        }

        private IEnumerable<TModel> By<TModel>(Func<User, bool> predicate) => this.context.Users.Include(u => u.FriendsAdded).Where(predicate).AsQueryable().ProjectTo<TModel>();
    }        
}