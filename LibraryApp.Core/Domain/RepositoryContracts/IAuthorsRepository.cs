using LibraryApp.Core.Domain.Entities;

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
    }
}
