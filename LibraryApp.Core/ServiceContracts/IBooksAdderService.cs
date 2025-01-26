using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.DTO;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for adding new book.
    /// </summary>
    public interface IBooksAdderService
    {
        /// <summary>
        /// Adds a book to the system.
        /// </summary>
        /// <param name="bookDTO">Data Transfer Object which contains data for creating the booksa.</param>
        /// <returns>True if book was added. Otherwise false.</returns>
        Task<Book> AddBook(BookDTO bookDTO);
    }
}
