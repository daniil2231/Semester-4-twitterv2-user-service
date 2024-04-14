using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TwitterV2Processing.User.Persistence
{
    public interface IUserRepository
    {
        Task<List<T>> GetAllUsers<T>(string dbName, string collectionName);
        Task<List<T>> GetFilteredUsers<T>(string dbName, string collectionName, FilterDefinition<T> filter);
        Task UpdateUser<T>(string dbName, string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> document);
        Task CreateUser<T>(string dbName, string collectionName, T document);
        Task DeleteUser<T>(string dbName, string collectionName, FilterDefinition<T> filter);
    }
}
