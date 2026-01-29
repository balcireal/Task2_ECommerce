using AutoMapper;
using ECommerceTask.Core.Entities;
using ECommerceTask.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace ECommerceTask.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IDistributedCache cache)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock
            };

            await _productRepository.AddAsync(product);

            await _cache.RemoveAsync("all_products");

            return product.Id;
        }
    }
}