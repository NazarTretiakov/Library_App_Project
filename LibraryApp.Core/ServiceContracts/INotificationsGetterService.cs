using LibraryApp.Core.Domain.Entities;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents service for retrieving notifications from the system
    /// </summary>
    public interface INotificationsGetterService
    {
        /// <summary>
        /// Retrieves all notifications from the system
        /// </summary>
        /// <returns>List of Notification objects</returns>
        Task<List<Notification>> GetAllNotifications();

        /// <summary>
        /// Retrieves all notifications of current user from the system
        /// </summary>
        /// <returns>List of Notification objects</returns>
        Task<List<Notification>> GetUserNotifications(string userId);

        /// <summary>
        /// Retrieves notification by it's id from the system
        /// </summary>
        /// <param name="notificationId">Id of notification</param>
        /// <returns>Notification object or null</returns>
        Task<Notification> GetNotification(string notificationId);
    }
}
