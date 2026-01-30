using ECommerceTask.Application.DTOs;
using ECommerceTask.Application.Wrappers;
using MediatR;

namespace ECommerceTask.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<ServiceResponse<ProductDto>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}