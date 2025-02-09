using LibraryApp.Core.Domain.Entities;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for changing the status of order.
    /// </summary>
    public interface IOrderStatusChangerService
    {
        /// <summary>
        /// Changes the status of order.
        /// </summary>
        /// <param name="order">Order object of which the status will be changed.</param>
        /// <param name="newStatus">New status of an order.</param>
        /// <returns></returns>
        Task<bool> ChangeStatus(Order order, string newStatus);
    }
}
