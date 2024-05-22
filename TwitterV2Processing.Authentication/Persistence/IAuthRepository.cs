using MongoDB.Driver;
using System.Net;
using TwitterV2Processing.Authentication.Models;

namespace TwitterV2Processing.Authentication.Persistence
{
    public interface IAuthRepository
    {
        Task<UserAccount> GetByUsername(string username);

        Task<DeleteResult> DeleteUserAccount(string username);

        Task CreateUserAccount(UserAccount userAccount);
    }
}
