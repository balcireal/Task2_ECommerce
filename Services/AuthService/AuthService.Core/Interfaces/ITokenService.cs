using AuthService.Core.Entities;

namespace AuthService.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}