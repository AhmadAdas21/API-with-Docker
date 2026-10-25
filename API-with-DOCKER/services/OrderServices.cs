using API_with_DOCKER.interfaces;
using API_with_DOCKER.models;

namespace API_with_DOCKER.services
{
    public class OrderServices:IOrder
    {
        public List<Order> GetAll()
        {
            Order.AsNoTracking.Any().ToList();
        }
    }
}
