using LibraryApp.Core.Domain.Entities;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for delete book.
    /// </summary>
    public interface IBooksRemoverService
    {
        /// <summary>
        /// Deletes book from the system.
        /// </summary>
        /// <param name="book">The book which will be deleted.</param>
        /// <returns>True if the book was deleted. Otherwise false.</returns>
        Task<bool> DeleteBook(Book book);
    }
}
