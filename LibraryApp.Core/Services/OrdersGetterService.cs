using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.Enums;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class OrdersGetterService : IOrdersGetterService
    {
        public readonly IOrdersRepository _ordersRepository;

        public OrdersGetterService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _ordersRepository.GetAllOrders();
        }

        public async Task<Order> GetOrderByOrderId(string orderId)
        {
            return await _ordersRepository.GetOrder(orderId);
        }

        public async Task<List<Order>> GetUserOrders(string userId, OrderStatusOptions? orderStatusOption = null)
        {
            if (orderStatusOption == null)
            {
                return await _ordersRepository.GetUserOrders(userId);
            }
            else
            {
                return await _ordersRepository.GetUserOrders(userId, orderStatusOption);
            }
        }
    }
}
