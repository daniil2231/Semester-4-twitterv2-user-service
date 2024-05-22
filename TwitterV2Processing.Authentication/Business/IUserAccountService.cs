using TwitterV2Processing.Authentication.Models;

namespace TwitterV2Processing.Authentication.Business
{
    public interface IUserAccountService
    {
        Task<UserAccount> GetByUsername(string username, string password);
    }
}
