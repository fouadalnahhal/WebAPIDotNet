using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPIDotNet.DTO;
using WebAPIDotNet.Models;
using WebAPIDotNet.Services;

namespace WebAPIDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _authenticationService;

        public AccountController(UserManager<ApplicationUser> userManager, ITokenService authenticationService)
        {
            _userManager = userManager;
            _authenticationService = authenticationService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto UserFromRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                UserName = UserFromRequest.UserName,
                Email = UserFromRequest.Email
            };

            IdentityResult identityResult = await _userManager.CreateAsync(user, UserFromRequest.Password);
            if (!identityResult.Succeeded)
            {
                identityResult.Errors.ToList().ForEach(err => ModelState.AddModelError(err.Code, err.Description));
                return BadRequest(ModelState);
            }

            return Ok("Create");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginDto UserFromRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(UserFromRequest.Email);
            if (user is null)
            {
                ModelState.AddModelError("Email", "Invalid email");
                return BadRequest(ModelState);
            }
            var found = await _userManager.CheckPasswordAsync(user, UserFromRequest.Password);
            if (!found)
            {
                ModelState.AddModelError("Password", "Invalid password");
                return BadRequest(ModelState);
            }

            var token = await _authenticationService.GenerateJwtTokenAsync(user);

            return Ok(new
            {
                token,
                expiration = DateTime.Now.AddHours(1)//mytoken.ValidTo
            });
        }
    }
}
