using Microsoft.AspNetCore.Mvc;

namespace API_with_DOCKER.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {

            return Ok("Get all products");
        }
    }
}
