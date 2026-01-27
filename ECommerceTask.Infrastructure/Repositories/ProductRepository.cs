using ECommerceTask.Core.Entities;
using ECommerceTask.Core.Interfaces;
using ECommerceTask.Infrastructure.Data;

namespace ECommerceTask.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}