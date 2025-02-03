using LibraryApp.Core.Domain.Entities;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for delete author.
    /// </summary>
    public interface IAuthorsRemoverService
    {
        /// <summary>
        /// Deletes author from the system.
        /// </summary>
        /// <param name="author">The author which will be deleted.</param>
        /// <returns>True if the author was deleted. Otherwise false.</returns>
        Task<bool> DeleteAuthor(Author author);
    }
}
