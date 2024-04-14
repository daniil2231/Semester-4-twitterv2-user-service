using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Xml.Linq;
using TwitterV2Processing.User.Models;
using TwitterV2Processing.User.Persistence;

namespace TwitterV2Processing.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository) {
            //_logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetAll() {
          return await _userRepository.GetAllUsers<UserModel>("admin", "users");
        }

        [HttpGet("GetByUsername")]
        public async Task<IEnumerable<UserModel>> GetByName(string name) {
          var filter = Builders<UserModel>.Filter.Eq("Username", name);
          return await _userRepository.GetFilteredUsers<UserModel>("admin", "users", filter);
        }

        //[HttpPatch("UpdateUsername")]
        //public async Task UpdateUsername(UserModel userData){
        //  var filter = Builders<UserModel>.Filter.Eq("Id", userData.Id);
        //  var update = Builders<UserModel>.Update.Set(x => x.Username, userData.Username);

        //  await _userRepository.UpdateUser<UserModel>("admin", "users", filter, update);
        //}

        [HttpPost]
        public async Task CreateUser(UserModel userData) {
          await _userRepository.CreateUser<UserModel>("admin", "users", userData);
        }

        [HttpDelete]
        public async Task DeleteUser(string id) {
          var filter = Builders<UserModel>.Filter.Eq("Id", id);
          await _userRepository.DeleteUser<UserModel>("admin", "users", filter);
        }
    }
}
