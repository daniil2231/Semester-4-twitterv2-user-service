using Microsoft.AspNetCore.Mvc;
using TwitterV2Processing.Authentication.Jwt;
using TwitterV2Processing.Authentication.Models;

namespace TwitterV2Processing.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly JwtTokenHandler _jwtTokenHandler;

        public AuthController(ILogger<AuthController> logger, JwtTokenHandler jwtTokenHandler)
        {
            _logger = logger;
            _jwtTokenHandler = jwtTokenHandler;
        }

        [HttpPost("login")]
        public ActionResult<AuthenticationResponse?> Authenticate([FromBody] AuthenticationRequest request)
        {
            var authenticationResponse = _jwtTokenHandler.GenerateJwtToken(request);
            if (authenticationResponse == null) return Unauthorized();
            return authenticationResponse;
        }
    }
}
