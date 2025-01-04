using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for adding new likes.
    /// </summary>
    public interface ILikesAdderService
    {
        /// <summary>
        /// Adds a like to the system.
        /// </summary>
        /// <param name="post">Post which was liked.</param>
        /// <param name="user">User that liked a post.</param>
        /// <returns>True if like was added. Otherwise false.</returns>
        Task<Like> AddLike(Post post, User user);
    }
}
