using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.DTO;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for adding new author.
    /// </summary>
    public interface IAuthorsAdderService
    {
        /// <summary>
        /// Adds an author to the system.
        /// </summary>
        /// <param name="authorDTO">Data Transfer Object which contains data for creating the author.</param>
        /// <returns>True if author was added. Otherwise false.</returns>
        Task<Author> AddAuthor(AuthorDTO authorDTO);
    }
}
