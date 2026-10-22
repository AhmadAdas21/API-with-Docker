using API_with_DOCKER.dtos.Product;
using API_with_DOCKER.models;

namespace API_with_DOCKER.interfaces
{
    public interface ProductInterface
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> CreateProductAsync(AddProduct dto);
    }
}
