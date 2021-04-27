using System;
using PhotoShare.Models;
using PhotoShare.Models.Enums;
using PhotoShare.Data;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Services
{    
    public class AlbumRoleService : IAlbumRoleService
    {
        private readonly PhotoShareContext context;

        public AlbumRoleService(PhotoShareContext context)
        {
            this.context = context;
        }

        public AlbumRole PublishAlbumRole(int albumId, int userId, string role)
        {
            Role roleAsEnum = Enum.Parse<Role>(role);
           
            AlbumRole albumRole = new AlbumRole()
            {
                AlbumId = albumId,
                UserId = userId,
                Role = roleAsEnum
            };

            this.context.AlbumRoles.Add(albumRole);
            this.context.SaveChanges();

            return albumRole;
        }
    }
}