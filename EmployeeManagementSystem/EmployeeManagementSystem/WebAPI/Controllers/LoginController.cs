using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] Users user)
        {
            var authenticatedUser = _userService.Authenticate(user.Username, user.Password);
            if (authenticatedUser == null)
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

            var token = _userService.GenerateToken(authenticatedUser);
            return Ok(new { token });
        }
    }
}
