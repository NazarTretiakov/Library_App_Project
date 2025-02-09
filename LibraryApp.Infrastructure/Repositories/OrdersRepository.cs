using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.Enums;
using LibraryApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly LibraryDbContext _db;

        public OrdersRepository(LibraryDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddOrder(Order order)
        {
            await _db.Orders.AddAsync(order);

            var rowsAdded = await _db.SaveChangesAsync();

            return rowsAdded > 0;
        }

        public async Task<bool> ChangeStatus(Order order, string newStatus)
        {
            if (newStatus == OrderStatusOptions.InRead.ToString() || newStatus == OrderStatusOptions.Delivered.ToString())
            {
                order.Status = newStatus;
                await _db.SaveChangesAsync();

                return true;
            }
            else if (newStatus == OrderStatusOptions.Returned.ToString())
            {
                order.Status = newStatus;

                order.Book.Holds = order.Book.Holds - 1;

                await _db.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _db.Orders.Include(o => o.Book)
                                   .ToListAsync();
        }

        public async Task<Order> GetOrder(string orderId)
        {
            return await _db.Orders.Include(o => o.Book)
                                   .Include(o => o.User)
                                   .FirstOrDefaultAsync(o => o.OrderId == Guid.Parse(orderId));
        }

        public async Task<List<Order>> GetUserOrders(string userId, OrderStatusOptions? orderStatusOption = null)
        {
            if (orderStatusOption != null)
            {
                return await _db.Orders.Include(o => o.Book)
                                         .ThenInclude(b => b.Author)
                                       .Where(o => o.User.Id == Guid.Parse(userId) && o.Status == orderStatusOption.ToString())
                                       .ToListAsync();
            }
            else
            {
                return await _db.Orders.Include(o => o.Book)
                                         .ThenInclude(b => b.Author)
                                       .Where(o => o.User.Id == Guid.Parse(userId))
                                       .ToListAsync();
            }
        }
    }
}
