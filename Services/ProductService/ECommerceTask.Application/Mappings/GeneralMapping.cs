using AutoMapper;
using ECommerceTask.Application.DTOs;
using ECommerceTask.Core.Entities;

namespace ECommerceTask.Application.Mappings
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            // CreateMap<User, UserDto>().ReverseMap(); 
        }
    }
}