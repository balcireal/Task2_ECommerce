using ECommerceTask.Core.Entities;

namespace ECommerceTask.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}