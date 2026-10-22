namespace API_with_DOCKER.models
{
    public class OrderItem
    {
        public int OrderItemId { get; init; }
        Order Order { get; set; }
        List<Product> Products { get; init; }
        private decimal ItemsTotalPrice { get; set; }
    }
}
