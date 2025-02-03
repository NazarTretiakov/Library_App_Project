using LibraryApp.Core.Domain.Entities;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for retrieving authors from db.
    /// </summary>
    public interface IAuthorsGetterService
    {
        /// <summary>
        /// Retrieves all authors from the system.
        /// </summary>
        /// <returns>List of Author objects.</returns>
        Task<List<Author>> GetAllAuthors();

        /// <summary>
        /// Retrieves author based on entered firstname and lastname from the system.
        /// </summary>
        /// <param name="firstname">Firstname of the author.</param>
        /// <param name="lastname">Lastname of the author.</param>
        /// <returns>Author object or null.</returns>
        Task<Author> GetAuthorByFullName(string firstname, string lastname);

        /// <summary>
        /// Retrieves author based on id from the system.
        /// </summary>
        /// <param name="authorId">Id of the author that will be retrieved.</param>
        /// <returns>Author object or null.</returns>
        Task<Author> GetAuthorByAuthorId(string authorId);

        /// <summary>
        /// Retrieves filtered authors from the system.
        /// </summary>
        /// <param name="searchString">The phrase to be searched for.</param>
        /// <param name="searchFilter">The name of the filter that will be used to search.</param>
        /// <returns>The list of Author objects or null.</returns>
        Task<List<Author>> GetFilteredAuthors(string searchFilter, string searchString);
    }
}
