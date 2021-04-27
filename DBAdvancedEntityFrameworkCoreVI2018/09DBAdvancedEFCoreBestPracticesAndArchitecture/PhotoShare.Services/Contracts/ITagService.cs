using PhotoShare.Models;

namespace PhotoShare.Services.Contracts
{    
    public interface ITagService
    {
        TModel ById<TModel>(int id);

        TModel ByName<TModel>(string name);

        bool Exists(int id);

        bool Exists(string name);

        Tag AddTag(string name);
    }
}