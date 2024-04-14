using TwitterV2Processing.User.Models;
using TwitterV2Processing.User.Persistence;

namespace TwitterV2Processing.User.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<UserModel> CreateUser(UserModel user)
        {
            return _userRepository.CreateUser(user);
        }

        public Task<UserModel> GetByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        }

        public Task<List<UserModel>> GetUsers()
        {
            return _userRepository.GetAllUsers();
        }
    }
}
