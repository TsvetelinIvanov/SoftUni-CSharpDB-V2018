using PhotoShare.Models;

namespace PhotoShare.Services.Contracts
{
    public interface IUserService
    {
        TModel ById<TModel>(int id);

        TModel ByUsername<TModel>(string username);

        TModel ByUsernameAndPassword<TModel>(string username, string password);

        bool Exists(int id);

        bool Exists(string name);

        bool IsDeleted(string name);

        bool IfHaveFriendship(int userId, int friendId);

        User Register(string username, string password, string email);

        void Delete(string username);

        Friendship AddFriend(int userId, int friendId);

        Friendship AcceptFriend(int userId, int friendId);

        bool UserExists(string username);

        bool CheckPassword(string username, string password);

        void ChangePassword(int userId, string password);

        void SetBornTown(int userId, int townId);

        void SetCurrentTown(int userId, int townId);                
    }
}