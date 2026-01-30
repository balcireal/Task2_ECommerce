using AutoMapper;
using ECommerceTask.Application.DTOs;
using ECommerceTask.Core.Entities;
using ECommerceTask.Application.Features.Products.Commands.CreateProduct;

namespace ECommerceTask.Application.Mappings
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
        }
    }
}