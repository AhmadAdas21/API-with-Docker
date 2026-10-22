using System.ComponentModel.DataAnnotations;
using API_with_DOCKER.models;

namespace API_with_DOCKER.dtos.Order
{
    public class CreateOrder
    {
        [Required]
        List<OrderItem> items;
        [Required]
        Customer Customer;
    }
}
