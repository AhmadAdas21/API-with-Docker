using API_with_DOCKER.DataBase;
using API_with_DOCKER.dtos.Product;
using API_with_DOCKER.interfaces;
using API_with_DOCKER.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_with_DOCKER.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class ProductController : ControllerBase
    {

        private readonly ProductInterface _productService;

        public ProductController(ProductInterface productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var Products = await _productService.GetAllProductsAsync();
            return StatusCode(StatusCodes.Status200OK, Products);

        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] AddProduct dto)
        {
            var product = await _productService.CreateProductAsync(dto);

            return StatusCode(201, product);
        }
    }
}
