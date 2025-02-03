using LibraryApp.Core.Domain.Entities;
using System.Linq.Expressions;

namespace LibraryApp.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Author entity.
    /// </summary>
    public interface IAuthorsRepository
    {
        /// <summary>
        /// Retrieves all authors from the data store.
        /// </summary>
        /// <returns>List of Author objects.</returns>
        Task<List<Author>> GetAllAuthors();

        /// <summary>
        /// Retrieves author based on entered firstname and lastname from the data store.
        /// </summary>
        /// <param name="firstname">Firstname of the author.</param>
        /// <param name="lastname">Lastname of the author.</param>
        /// <returns>Author object or null.</returns>
        Task<Author> GetAuthor(string firstname, string lastname);

        /// <summary>
        /// Retrieves author based on id from the data store.
        /// </summary>
        /// <param name="authorId">Id of the author that will be retrieved.</param>
        /// <returns>Author object or null.</returns>
        Task<Author> GetAuthor(string authorId);

        /// <summary>
        /// Retrieves filtered authors from the data store.
        /// </summary>
        /// <param name="predicate">LINQ expression to filter authors that will be retrieved.</param>
        /// <returns>List of Author objects or null.</returns>
        Task<List<Author>> GetFilteredAuthors(Expression<Func<Author, bool>> predicate);

        /// <summary>
        /// Adds author to the data store.
        /// </summary>
        /// <param name="author">Author object that will be added to the data store.</param>
        /// <returns>True if author was added. Otherwise false.</returns>
        Task<bool> AddAuthor(Author author);

        /// <summary>
        /// Deletes author from the data store.
        /// </summary>
        /// <param name="author">The author object which will be deleted.</param>
        /// <returns>True if the author was deleted. Otherwise false.</returns>
        Task<bool> DeleteAuthor(Author author);
    }
}
