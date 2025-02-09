using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Enums;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for retrieving orders from db.
    /// </summary>
    public interface IOrdersGetterService
    {
        /// <summary>
        /// Retrieves all orders from the system.
        /// </summary>
        /// <returns>List of Order objects.</returns>
        Task<List<Order>> GetAllOrders();

        /// <summary>
        /// Retrieves order from the system.
        /// </summary>
        /// <param name="orderId">The id of order that will be retrieved.</param>
        /// <returns>Order object or null.</returns>
        Task<Order> GetOrderByOrderId(string orderId);

        /// <summary>
        /// Retrieves all user's orders from the system.
        /// </summary>
        /// <param name="userId">The id of the user which orders will be retrieved.</param>
        /// <param name="orderStatusOption">The order status of orders that will be retrieved. If not entered, will be retrieved all orders</param>
        /// <returns>The list of Order objects or null.</returns>
        Task<List<Order>> GetUserOrders(string userId, OrderStatusOptions? orderStatusOption = null);
    }
}
