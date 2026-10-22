namespace API_with_DOCKER.models
{
    public class Customer
    {
        public int customerId { get; set; }
        public string Name { get; set; }
        Order Order { get; set; }
        
    }
}
