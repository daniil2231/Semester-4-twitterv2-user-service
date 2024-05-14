using MongoDB.Bson;
using MongoDB.Driver;
using TwitterV2Processing.User.DbSettings;
using TwitterV2Processing.User.Models;

namespace TwitterV2Processing.User.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserModel> _users;
        public UserRepository(IDatabaseSettings settings, IMongoClient client) {
            var db = client.GetDatabase(settings.DatabaseName);
            _users = db.GetCollection<UserModel>(settings.UsersCollectionName);
        }

        public async Task<UserModel> CreateUser(UserModel user)
        {
            await _users.InsertOneAsync(user);
            return user;
        }

        public async Task<DeleteResult> DeleteUser(string username)
        {
            return await _users.DeleteOneAsync(user => user.Username.Equals(username));
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _users.Find(user => true).ToListAsync();
        }

        public async Task<UserModel> GetByUsername(string username)
        {
            return await _users.Find<UserModel>(user => user.Username == username).FirstOrDefaultAsync();
        }

        public async Task UpdateUser(ObjectId id, UserModel user)
        {
            await _users.ReplaceOneAsync(user => user.Id.Equals(id), user);
        }
    }
}
