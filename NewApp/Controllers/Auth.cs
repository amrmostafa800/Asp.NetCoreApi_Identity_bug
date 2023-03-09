using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewApp.DTOs;
using NewApp.Helpers;
using NewApp.Models;

namespace NewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        private IConfiguration _configuration;

        public Auth(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDTO NewUser)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = NewUser.Username, Email = NewUser.Email };
                var result = await _userManager.CreateAsync(user, NewUser.Password);
                if (result.Succeeded)
                {
                    return Ok("OK");
                }

                return BadRequest(result.Errors);
            }
            return BadRequest("Unknown Error");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(username);

                string JWT = JWT_Helper.Generate(_configuration, username);

                await _userManager.SetAuthenticationTokenAsync(user!, "JWT", "JWT", JWT);
                return Ok(JWT);
            }

            return BadRequest("Invalid username or password");
        }

        [HttpPost("TestAuth")]
        [Authorize]
        public IActionResult TestAuth()
        {
            return Ok("true");
        }
    }
}
