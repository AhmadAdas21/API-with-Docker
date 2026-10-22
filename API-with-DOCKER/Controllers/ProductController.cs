using API_with_DOCKER.DataBase;
using API_with_DOCKER.dtos.Product;
using API_with_DOCKER.interfaces;
using API_with_DOCKER.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_with_DOCKER.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {

        private readonly IProduct _productService;

        public ProductController(IProduct productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts()
        {
            var Products = await _productService.GetAllProductsAsync();
            return StatusCode(StatusCodes.Status200OK, Products);

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] AddProduct Request)
        {
            {
                var product = await _productService.CreateProductAsync(Request);

                return StatusCode(201, product);
            }
        }
    }
}
