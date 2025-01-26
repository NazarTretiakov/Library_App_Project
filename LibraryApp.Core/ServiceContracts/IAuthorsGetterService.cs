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
    }
}
