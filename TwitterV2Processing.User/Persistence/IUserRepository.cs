using MongoDB.Bson;
using MongoDB.Driver;
using TwitterV2Processing.User.Models;

namespace TwitterV2Processing.User.Persistence
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAllUsers();
        Task<UserModel> GetByUsername(string username);
        Task<UserModel> CreateUser(UserModel user);
        Task UpdateUser(ObjectId id, UserModel user);
        Task<DeleteResult> DeleteUser(string username);
    }
}
