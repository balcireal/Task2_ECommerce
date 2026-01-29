using ECommerceTask.Core.Entities;

namespace ECommerceTask.Core.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}