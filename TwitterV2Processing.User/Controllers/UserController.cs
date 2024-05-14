using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, ILogger<UserController> logger, IConfiguration configuration) {
            _logger = logger;
            _userService = userService;
            _configuration = configuration;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var users = await _userService.GetUsers();
            
            return Ok(users);
        }

        // Placeholder Login method
        [HttpGet("GetByUsername")]
        public async Task<IActionResult> GetByName(string name, string password) {
            var user = await _userService.GetByUsername(name, password);
            
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserModel user) {
            var newUser = await _userService.CreateUser(user);
            
            return Ok(newUser);
        }

        [HttpDelete]
        public async Task<DeleteResult> DeleteUser(string username)
        {
            var res = await _userService.DeleteUser(username);

            return res;
        }
    }
}
