using MongoDB.Driver;
using TwitterV2Processing.User.Models;

namespace TwitterV2Processing.User.Business
{
    public interface IUserService
    {
        public Task<List<UserModel>> GetUsers();

        public Task<UserModel> GetByUsername(string username, string password);

        public Task<UserModel> CreateUser(UserModel user);

        public Task<DeleteResult> DeleteUser(string username);
    }
}
