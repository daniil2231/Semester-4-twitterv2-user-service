using MongoDB.Driver;
using System.Net;
using TwitterV2Processing.Authentication.DbSettings;
using TwitterV2Processing.Authentication.Models;

namespace TwitterV2Processing.Authentication.Persistence
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IMongoCollection<UserAccount> _users;

        public AuthRepository(IDatabaseSettings settings, IMongoClient client)
        {
            var db = client.GetDatabase(settings.DatabaseName);
            _users = db.GetCollection<UserAccount>(settings.UsersCollectionName);
        }

        public async Task CreateUserAccount(UserAccount userAccount)
        {
            await _users.InsertOneAsync(userAccount);
        }

        public async Task<DeleteResult> DeleteUserAccount(string username)
        {
            return await _users.DeleteOneAsync(user => user.Username.Equals(username));
        }

        public async Task<UserAccount> GetByUsername(string username)
        {
            return await _users.Find<UserAccount>(user => user.Username == username).FirstOrDefaultAsync();
        }
    }
}
