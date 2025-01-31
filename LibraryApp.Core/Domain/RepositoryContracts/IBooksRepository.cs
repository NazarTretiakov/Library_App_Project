using LibraryApp.Core.Domain.Entities;
using System.Linq.Expressions;

namespace LibraryApp.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Book entity.
    /// </summary>
    public interface IBooksRepository
    {
        /// <summary>
        /// Retrieves all books from the data store.
        /// </summary>
        /// <returns>The list of Book objects.</returns>
        Task<List<Book>> GetAllBooks();

        /// <summary>
        /// Retrieves book from the data store.
        /// </summary>
        /// <param name="bookId">Id of the book that will be retrieved.</param>
        /// <returns>Book object or null.</returns>
        Task<Book> GetBook(string bookId);

        /// <summary>
        /// Retrieves filtered books from the data store.
        /// </summary>
        /// <param name="predicate">LINQ expression to filter books that will be retrieved.</param>
        /// <returns>List of Book objects or null.</returns>
        Task<List<Book>> GetFilteredBooks(Expression<Func<Book, bool>> predicate);

        /// <summary>
        /// Adds book to the data store.
        /// </summary>
        /// <param name="book">Book object that will be added to the data store.</param>
        /// <returns>True if book was added. Otherwise false.</returns>
        Task<bool> AddBook(Book book);

        /// <summary>
        /// Changes book amount in the data store.
        /// </summary>
        /// <param name="book">The book object of which amount will be changed.</param>
        /// <param name="newAmount">The new amount of the book.</param>
        /// <returns>Book object.</returns>
        Task<Book> ChangeBookAmount(Book book, int newAmount);

        /// <summary>
        /// Deletes book from the data store.
        /// </summary>
        /// <param name="book">The book object which will be deleted.</param>
        /// <returns>True if the book was deleted. Otherwise false.</returns>
        Task<bool> DeleteBook(Book book);

        /// <summary>
        /// Updates rating of the book.
        /// </summary>
        /// <param name="book">The book object of which rating will be updated.</param>
        /// <returns></returns>
        Task UpdateRating(Book book);
    }
}
