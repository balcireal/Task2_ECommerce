using AutoMapper;
using ECommerceTask.Application.DTOs;
using ECommerceTask.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text;

namespace ECommerceTask.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper, IDistributedCache cache)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = "all_products";

            var cachedData = await _cache.GetAsync(cacheKey);
            if (cachedData != null)
            {
                var serializedData = Encoding.UTF8.GetString(cachedData);
                return JsonSerializer.Deserialize<IEnumerable<ProductDto>>(serializedData);
            }

            var products = await _productRepository.GetAllAsync();
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                .SetSlidingExpiration(TimeSpan.FromMinutes(2));

            var serializedProducts = JsonSerializer.Serialize(productDtos);
            await _cache.SetAsync(cacheKey, Encoding.UTF8.GetBytes(serializedProducts), options);

            return productDtos;
        }
    }
}