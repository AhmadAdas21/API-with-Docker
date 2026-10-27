using API_with_DOCKER.DataBase;
using API_with_DOCKER.DesignsPatterns.Specification;
using API_with_DOCKER.dtos.Product;
using API_with_DOCKER.interfaces;
using API_with_DOCKER.models;
using API_with_DOCKER.DesignsPatterns.Specification;
using Microsoft.EntityFrameworkCore;

namespace API_with_DOCKER.services
{
    public class ProductServices:IProductServices
    {

        private readonly AppDbContext AppDbContext;

        public ProductServices(AppDbContext context)
        {
            AppDbContext = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await AppDbContext.Products.AsNoTracking().ToListAsync();
        }
        public async Task<Product> CreateProductAsync(AddProduct dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity
            };

            AppDbContext.Products.Add(product);

            await AppDbContext.SaveChangesAsync();

            return product;
        }

        public async Task<List<Product>> SearchProductsAsync(
            decimal minPrice,
            decimal maxPrice)
        {
            return await AppDbContext.Products
                .AsNoTracking()
                .Where(ProductSpecifications.Active())
                .Where(ProductSpecifications.PriceInRange(minPrice, maxPrice))
                .Where(ProductSpecifications.InStock())
                .ToListAsync();
        }

    }
}
