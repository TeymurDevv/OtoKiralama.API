using OtoKiralama.Persistance.Entities;
using System.Security.Claims;

namespace OtoKiralama.Application.Interfaces
{
    public interface ITokenService
    {
        string GetToken(string SecretKey, string Audience, string Issuer, AppUser existUser, IList<string> roles);
        ClaimsPrincipal ValidateToken(string token);
    }
}
