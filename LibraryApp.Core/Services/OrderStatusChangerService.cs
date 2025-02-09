using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.Enums;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class OrderStatusChangerService : IOrderStatusChangerService
    {
        public readonly IOrdersRepository _ordersRepository;

        public OrderStatusChangerService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<bool> ChangeStatus(Order order, string newStatus)
        {
            if (newStatus == OrderStatusOptions.InRead.ToString() || newStatus == OrderStatusOptions.Delivered.ToString() || newStatus == OrderStatusOptions.Returned.ToString())
            {
                return await _ordersRepository.ChangeStatus(order, newStatus);
            }
            else
            {
                return false;
            }
        }
    }
}
