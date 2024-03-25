using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser()
        {
            var maxCountOrders = await _context.Orders
                .GroupBy(order => order.UserId)
                .OrderByDescending(group => group.Count())
                .Select(group => group.Key)
                .FirstOrDefaultAsync();

            var user = await _context.Users
                .Where(u => u.Id == maxCountOrders)
                .Include(u => u.Orders)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _context.Users
                .Where(user => user.Status == Enums.UserStatus.Inactive)
                .Include(user => user.Orders)
                .ToListAsync();

            return users;
        }
    }
}
