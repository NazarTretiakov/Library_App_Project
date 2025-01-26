using LibraryApp.Core.Domain.Entities;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for retrieving books from db.
    /// </summary>
    public interface IBooksGetterService
    {
        /// <summary>
        /// Retrieves all books from the system.
        /// </summary>
        /// <returns>List of Book objects.</returns>
        Task<List<Book>> GetAllBooks();

        /// <summary>
        /// Retrieves book from the system.
        /// </summary>
        /// <param name="bookId">The id of book that will be retrieved.</param>
        /// <returns>Book object or null.</returns>
        Task<Book> GetBookByBookId(string bookId);

        /// <summary>
        /// Retrieves filtered books from the system.
        /// </summary>
        /// <param name="searchString">The phrase to be searched for.</param>
        /// <param name="searchFilter">The name of the filter that will be used to search.</param>
        /// <returns>The list of Book objects or null.</returns>
        Task<List<Book>> GetFilteredBooks(string searchFilter, string searchString);
    }
}
