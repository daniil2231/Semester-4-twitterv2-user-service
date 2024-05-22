using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TwitterV2Processing.User.Business;
using TwitterV2Processing.User.Models;

namespace TwitterV2Processing.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(IUserService userService, ILogger<UserController> logger) {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var users = await _userService.GetUsers();
            
            return Ok(users);
        }

        [HttpGet("GetByUsername")]
        public async Task<IActionResult> GetByName(string name) {
            var user = await _userService.GetByUsername(name);
            
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserModel user) {
            var newUser = await _userService.CreateUser(user);
            
            return Ok(newUser);
        }

        [HttpDelete]
        [Authorize]
        public async Task<DeleteResult> DeleteUser(string username)
        {
            var res = await _userService.DeleteUser(username);

            return res;
        }
    }
}
