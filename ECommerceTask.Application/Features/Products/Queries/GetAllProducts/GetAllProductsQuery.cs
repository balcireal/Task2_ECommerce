using ECommerceTask.Application.DTOs;
using MediatR;

namespace ECommerceTask.Application.Features.Products.Queries.GetAllProducts
{
    
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
    }
}