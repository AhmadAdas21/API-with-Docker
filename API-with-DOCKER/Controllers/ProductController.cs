using API_with_DOCKER.DataBase;
using API_with_DOCKER.dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_with_DOCKER.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class ProductController : ControllerBase
    {

        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.AsNoTracking().ToListAsync();

            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] AddProduct product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProducts), new { id = product.id }, product);
        }
    }
}
