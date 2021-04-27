using PhotoShare.Models;

namespace PhotoShare.Services.Contracts
{    
    public interface IAlbumTagService
    {
        AlbumTag AddTagTo(int albumId, int tagId);
    }
}