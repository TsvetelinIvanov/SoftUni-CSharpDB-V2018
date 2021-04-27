using PhotoShare.Models;

namespace PhotoShare.Services.Contracts
{
    public interface IAlbumService
    {
        TModel ById<TModel>(int id);

        TModel ByName<TModel>(string name);

        bool Exists(int id);

        bool Exists(string name);

        Album Create(int userId, string albumTitle, string BgColor, string[] tags);
    }
}