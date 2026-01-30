using AutoMapper;
using ECommerceTask.Application.DTOs;
using ECommerceTask.Application.Wrappers;
using ECommerceTask.Core.Entities;
using ECommerceTask.Core.Interfaces;
using ECommerceTask.Core.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace ECommerceTask.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ServiceResponse<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IDistributedCache _cache;

        public CreateProductCommandHandler(
            IProductRepository productRepository,
            IMapper mapper,
            IPublishEndpoint publishEndpoint,
            IDistributedCache cache)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _cache = cache;
        }

        public async Task<ServiceResponse<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);

            await _productRepository.AddAsync(product);

            await _publishEndpoint.Publish(new ProductCreatedEvent
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CreatedDate = DateTime.UtcNow
            }, cancellationToken);

            await _cache.RemoveAsync("all_products", cancellationToken);

            var productDto = _mapper.Map<ProductDto>(product);
            return new ServiceResponse<ProductDto>(productDto);
        }
    }
}