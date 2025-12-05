using System.Security.Claims;
using WebAPIDotNet.Models;

namespace WebAPIDotNet.Services
{
    public interface ITokenService
    {
        Task<string> GenerateJwtTokenAsync(ApplicationUser user);

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
