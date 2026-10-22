namespace API_with_DOCKER.models
{
    public class Order
    {
        public int orderID { get; set; }
        List<OrderItem> Items { get; set; }
        Customer Customer { get; set; }
        public decimal TotalCost { get; set; }


    }
}
