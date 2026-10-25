using API_with_DOCKER.dtos.Product;
using API_with_DOCKER.interfaces;
using API_with_DOCKER.models;

namespace API_with_DOCKER.DesignsPatterns.Decorater
{
    public class CachedProductService:IProductServices
    {
        private readonly IProductServices _inner;
        public CachedProductService(IProductServices inner)
        {
            _inner = inner;
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _inner.GetAllProductsAsync();
        }
        public async Task<Product> CreateProductAsync(AddProduct request)
        {
            return await _inner.CreateProductAsync(request);
        }
    }
}
