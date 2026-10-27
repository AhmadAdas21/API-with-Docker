using System.Text.Json;
using API_with_DOCKER.dtos.Product;
using API_with_DOCKER.interfaces;
using API_with_DOCKER.models;
using Microsoft.Extensions.Caching.Distributed;

namespace API_with_DOCKER.DesignsPatterns.Decorater
{
    public class CachedProductService : IProductServices
    {
        private readonly IProductServices _inner;
        private readonly IDistributedCache _cache;
        private readonly ILogger<CachedProductService> _logger;

       
        private const string CacheKey = "AllProductsCache";

        public CachedProductService(IProductServices inner, IDistributedCache cache, ILogger<CachedProductService> logger)
        {
            _inner = inner;
            _cache = cache;
            _logger = logger;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var cachedData = await _cache.GetStringAsync(CacheKey);

            if (cachedData != null)
            {
                var cachedProducts = JsonSerializer.Deserialize<List<Product>>(cachedData);

                if (cachedProducts != null)
                {
                    _logger.LogInformation("cache hit");
                    return cachedProducts;
                }
            }
            _logger.LogInformation("cache missed");

            var products = await _inner.GetAllProductsAsync();

            await _cache.SetStringAsync(CacheKey, JsonSerializer.Serialize(products),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow =TimeSpan.FromMinutes(10)
                });

            return products;
        }

        public async Task<Product> CreateProductAsync(AddProduct request)
        {
            return await _inner.CreateProductAsync(request);
        }
    }
}
