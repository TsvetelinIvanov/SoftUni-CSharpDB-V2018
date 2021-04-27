using PhotoShare.Models;

namespace PhotoShare.Services.Contracts
{
    public interface IAlbumRoleService
    {
        AlbumRole PublishAlbumRole(int albumId, int userId, string role);
    }
}