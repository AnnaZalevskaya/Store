using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrder()
        {
            var selectedOrder = await _context.Orders
                .Where(order => order.Price ==
                    _context.Orders.Max(order => order.Price))
                .Include(order => order.User)
                .FirstOrDefaultAsync();

            return selectedOrder;
        }

        public async Task<List<Order>> GetOrders()
        {
            var selectedOrders = await _context.Orders
                .Where(order => order.Quantity > 10)
                .Include(order => order.User)
                .ToListAsync();

            return selectedOrders;
        }
    }
}
