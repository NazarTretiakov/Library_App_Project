using LibraryApp.Core.Domain.Entities;

namespace LibraryApp.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Notification entity
    /// </summary>
    public interface INotificationsRepository
    {
        /// <summary>
        /// Returns all notifications from the data store
        /// </summary>
        /// <returns>List of Notification objects</returns>
        Task<List<Notification>> GetAllNotifications();

        /// <summary>
        /// Returns all user notifications from the data store
        /// </summary>
        /// <returns>List of Notification objects</returns>
        Task<List<Notification>> GetUserNotifications(string userId);

        /// <summary>
        /// Returns notification by it's id from the data store
        /// </summary>
        /// <param name="notificationId">Id of notification</param>
        /// <returns>Notification object or null</returns>
        Task<Notification> GetNotification(string notificationId);

        /// <summary>
        /// Adds notification to the data store
        /// </summary>
        /// <param name="notification">Notification object that will be added to the data store</param>
        /// <returns>True if notification was added. Otherwise false</returns>
        Task<bool> CreateNotification(Notification notification);
    }
}
