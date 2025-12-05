using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPIDotNet.Models;

namespace WebAPIDotNet.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenService(IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public async Task<string> GenerateJwtTokenAsync(ApplicationUser userFromDb)
        {
            List<Claim> UserClaims = new List<Claim>();

            //Token Genrated id change (JWT Predefind Claims )
            UserClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            UserClaims.Add(new Claim(ClaimTypes.NameIdentifier, userFromDb.Id));
            UserClaims.Add(new Claim(ClaimTypes.Name, userFromDb.UserName));

            var userROles = await _userManager.GetRolesAsync(userFromDb);
            foreach (var role in userROles)
            {
                UserClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var SignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecritKey"]));

            //design token
            JwtSecurityToken token = new JwtSecurityToken(
                audience: _config["JWT:AudienceIP"],
                issuer: _config["JWT:IssuerIP"],
                expires: DateTime.Now.AddHours(1),
                claims: UserClaims,
                signingCredentials: new SigningCredentials(SignInKey, SecurityAlgorithms.HmacSha256Signature));

            //generate token response

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
