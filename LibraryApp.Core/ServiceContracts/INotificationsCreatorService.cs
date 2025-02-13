using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.DTO;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for adding new notification.
    /// </summary>
    public interface INotificationsCreatorService
    {
        /// <summary>
        /// Adds a notification to the system.
        /// </summary>
        /// <param name="notificationDTO">Data Transfer Object which contains data for creating the notification.</param>
        /// <returns>True if notification was added. Otherwise false.</returns>
        Task<Notification> CreateNotification(NotificationDTO notificationDTO);

        /// <summary>
        /// Adds notifications for admin and every librarian to the system.
        /// </summary>
        /// <param name="notificationForAdminsDTO">Data Transfer Object which contains data for creating the notification.</param>
        /// <returns>True if notifications was added. Otherwise false.</returns>
        Task<bool> CreateNotificationForAdmins(NotificationForAdminsDTO notificationForAdminsDTO);
    }
}
