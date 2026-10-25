using API_with_DOCKER.dtos.Order;
using API_with_DOCKER.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_with_DOCKER.Controllers
{
    [ApiController]
    public class OrderController:ControllerBase
    {
        IOrderServices orderInterface;
        public OrderController(IOrderServices orderInterface)
        {
            this.orderInterface = orderInterface;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var Orders = orderInterface.GetAll();

            return Ok(Orders);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrder dto)
        {

        }


    }
}
