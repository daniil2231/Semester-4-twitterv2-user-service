using PasswordHashing;
using TwitterV2Processing.Authentication.Models;
using TwitterV2Processing.Authentication.Persistence;

namespace TwitterV2Processing.Authentication.Business
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IAuthRepository _authRepository;

        public UserAccountService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public Task<UserAccount> GetByUsername(string username, string password)
        {
            Task<UserAccount> returnedUser = _authRepository.GetByUsername(username);
            if (PasswordHasher.Validate(password, returnedUser.Result.Password))
            {
                return _authRepository.GetByUsername(username);
            }
            else
            {
                return null;
            }
        }
    }
}
