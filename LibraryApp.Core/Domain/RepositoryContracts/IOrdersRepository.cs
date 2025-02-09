using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Enums;
using System.Linq.Expressions;

namespace LibraryApp.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Order entity.
    /// </summary>
    public interface IOrdersRepository
    {
        /// <summary>
        /// Retrieves all orders from the data store.
        /// </summary>
        /// <returns>The list of Order objects.</returns>
        Task<List<Order>> GetAllOrders();

        /// <summary>
        /// Retrieves order from the data store.
        /// </summary>
        /// <param name="orderId">Id of the order that will be retrieved.</param>
        /// <returns>Order object or null.</returns>
        Task<Order> GetOrder(string orderId);

        /// <summary>
        /// Adds order to the data store.
        /// </summary>
        /// <param name="order">Order object that will be added to the data store.</param>
        /// <returns>True if order was added. Otherwise false.</returns>
        Task<bool> AddOrder(Order order);

        /// <summary>
        /// Retrieves all user's orders from the data store.
        /// </summary>
        /// <param name="userId">The id of the user which orders will be retrieved.</param>
        /// <param name="orderStatusOption">The order status of orders that will be retrieved. If not entered, will be retrieved all orders</param>
        /// <returns>The list of Order objects or null.</returns>
        Task<List<Order>> GetUserOrders(string userId, OrderStatusOptions? orderStatusOption = null);

        /// <summary>
        /// Changes the status of order.
        /// </summary>
        /// <param name="order">Order object of which the status will be changed.</param>
        /// <param name="newStatus">New status of an order.</param>
        /// <returns></returns>
        Task<bool> ChangeStatus(Order order, string newStatus);
    }
}
