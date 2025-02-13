using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class NotificationsGetterService : INotificationsGetterService
    {
        private readonly INotificationsRepository _notificationsRepository;

        public NotificationsGetterService(INotificationsRepository notificationsRepository)
        {
            _notificationsRepository = notificationsRepository;
        }

        public async Task<List<Notification>> GetAllNotifications()
        {
            return await _notificationsRepository.GetAllNotifications();
        }

        public async Task<Notification> GetNotification(string notificationId)
        {
            return await _notificationsRepository.GetNotification(notificationId);
        }

        public async Task<List<Notification>> GetUserNotifications(string userId)
        {
            return await _notificationsRepository.GetUserNotifications(userId);
        }
    }
}
