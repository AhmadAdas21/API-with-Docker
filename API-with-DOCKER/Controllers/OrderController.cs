using System.Threading;
using API_with_DOCKER.dtos.Order;
using API_with_DOCKER.interfaces;
using API_with_DOCKER.models;
using API_with_DOCKER.services;
using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;

namespace API_with_DOCKER.Controllers
{
    [ApiController]
    public class OrderController:ControllerBase
    {
        IOrderServices _orderService;
        public OrderController(IOrderServices orderInterface)
        {
            this._orderService = orderInterface;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetAllOrdersAsync(cancellationToken);

            return Ok(orders.Select(ToResponse).ToList());
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrder request, CancellationToken cancellationToken)
        {
            var order = await _orderService.CreateOrderAsync(request, cancellationToken);

            return StatusCode( StatusCodes.Status201Created, ToResponse(order));
        }
        private static object ToResponse(Order order)
        {
            return new
            {
                order.Id,
                order.CustomerId,
                order.PricingType,
                order.Subtotal,
                order.TotalCost,
                order.CreatedAtUtc,

                Items = order.Items.Select(item => new
                {
                    item.ProductId,
                    item.Quantity,
                    item.UnitPrice,
                    item.TotalPrice
                }).ToList()
            };
        }

    }
}
