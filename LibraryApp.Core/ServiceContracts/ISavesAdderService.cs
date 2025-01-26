using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for adding new saves.
    /// </summary>
    public interface ISavesAdderService
    {
        /// <summary>
        /// Adds a save of the post to the system.
        /// </summary>
        /// <param name="post">Post which was saved.</param>
        /// <param name="user">User that saved a post.</param>
        /// <returns>True if save was added. Otherwise false.</returns>
        Task<Save> AddPostSave(Post post, User user);

        /// <summary>
        /// Adds a save of the book to the system.
        /// </summary>
        /// <param name="book">Book which was saved.</param>
        /// <param name="user">User that saved a book.</param>
        /// <returns>True if save was added. Otherwise false.</returns>
        Task<Save> AddBookSave(Book book, User user);
    }
}
