using OtoKiralama.Persistance.Entities;

namespace OtoKiralama.Application.Interfaces
{
    public interface ITokenService
    {
        string GetToken(string SecretKey, string Audience, string Issuer, AppUser existUser, IList<string> roles);
    }
}
