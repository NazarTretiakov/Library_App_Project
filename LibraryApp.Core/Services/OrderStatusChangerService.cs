using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.DTO;
using LibraryApp.Core.Enums;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class OrderStatusChangerService : IOrderStatusChangerService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly INotificationsCreatorService _notificationsCreatorService;

        public OrderStatusChangerService(IOrdersRepository ordersRepository, INotificationsCreatorService notificationsCreatorService)
        {
            _ordersRepository = ordersRepository;
            _notificationsCreatorService = notificationsCreatorService;
        }

        public async Task<bool> ChangeStatus(Order order, string newStatus)
        {
            if (newStatus == OrderStatusOptions.InRead.ToString() || newStatus == OrderStatusOptions.Delivered.ToString() || newStatus == OrderStatusOptions.Returned.ToString())
            {
                bool result = await _ordersRepository.ChangeStatus(order, newStatus);

                if (newStatus == OrderStatusOptions.Delivered.ToString())
                {
                    await _notificationsCreatorService.CreateNotification(new NotificationDTO()
                    {
                        Content = $"Book {order.Book.Title} has been delivered, you can receive it in library.",
                        ReceiverId = order.User.Id.ToString(),
                        ObjectId = order.Book.BookId.ToString()
                    });
                }

                return result;
            }
            else
            {
                return false;
            }
        }
    }
}
