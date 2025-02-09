using LibraryApp.Core.Domain.Entities;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for creating new orders.
    /// </summary>
    public interface IOrdersCreatorService
    {
        /// <summary>
        /// Adds a order of the current working user to the system.
        /// </summary>
        /// <param name="bookId">Id of the book which was ordered.</param>
        /// <returns>The created object of the order.</returns>
        Task<Order> CreateOrder(string bookId);
    }
}
