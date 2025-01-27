using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.DTO;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for changing amount of book.
    /// </summary>
    public interface IChangeBookAmountService
    {
        /// <summary>
        /// Changes book amount in the system.
        /// </summary>
        /// <param name="book">The book object of which amount will be changed.</param>
        /// <param name="newAmount">The new amount of the book.</param>
        /// <returns>Book object.</returns>
        Task<Book> ChangeBookAmount(Book book, int newAmount);
    }
}
