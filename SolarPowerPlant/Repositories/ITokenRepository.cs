using Microsoft.AspNetCore.Identity;

namespace SolarPowerAPI.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser identityUser, List<string> roles);
    }
}
