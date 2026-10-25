
using API_with_DOCKER.DataBase;
using API_with_DOCKER.DesignsPatterns.Pricing;
using API_with_DOCKER.dtos.Order;
using API_with_DOCKER.interfaces;
using API_with_DOCKER.models;
using Microsoft.EntityFrameworkCore;

namespace API_with_DOCKER.services
{
    public class OrderServices : IOrderServices
    {
        private readonly AppDbContext _context;
        private readonly IPricingStrategyResolver _pricingResolver;

        public OrderServices(AppDbContext context, IPricingStrategyResolver pricingResolver)
        {
            _context = context;
            _pricingResolver = pricingResolver;
        }

        public async Task<List<Order>> GetAllOrdersAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Orders.AsNoTracking().Include(order => order.Items).ToListAsync(cancellationToken);
        }

        public async Task<Order> CreateOrderAsync( CreateOrder request,CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (request.CustomerId <= 0)
            {
                throw new ArgumentException("Invalid customer ID.");
            }

            if (request.Items == null || request.Items.Count == 0)
            {
                throw new ArgumentException( "Order must contain at least one item.");
            }

            if (request.Items.Any(item =>
                item == null ||item.ProductId <= 0 || item.Quantity <= 0))
            {
                throw new ArgumentException( "Invalid product ID or quantity.");
            }

            bool customerExists = await _context.Customers.AnyAsync(customer => customer.Id == request.CustomerId,cancellationToken);

            if (!customerExists)
            {
                throw new KeyNotFoundException("Customer not found.");
            }

            var productIds = request.Items.Select(item => item.ProductId).ToList();

            if (productIds.Count != productIds.Distinct().Count())
            {
                throw new ArgumentException("Duplicate products are not allowed.");
            }

            var products = await _context.Products.AsNoTracking().Where(product => productIds.Contains(product.Id))
                .ToDictionaryAsync(product => product.Id,cancellationToken);

            if (products.Count != productIds.Count)
            {
                throw new KeyNotFoundException("One or more products were not found.");
            }

            List<OrderItem> orderItems = new();

            foreach (var item in request.Items)
            {
                var product = products[item.ProductId];

                orderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });
            }

            decimal subtotal = orderItems.Sum(item => item.TotalPrice);

            IPricingStrategy strategy = _pricingResolver.Resolve(request.PricingType);

            decimal finalPrice = strategy.CalculatePrice(subtotal);

            finalPrice = Math.Round(finalPrice,2,MidpointRounding.AwayFromZero);

            Order order = new Order
            {
                CustomerId = request.CustomerId,
                Items = orderItems,
                PricingType = request.PricingType,
                Subtotal = subtotal,
                TotalCost = finalPrice,
                CreatedAtUtc = DateTime.UtcNow
            };

            await _context.Orders.AddAsync( order, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return order;
        }
    }
}
