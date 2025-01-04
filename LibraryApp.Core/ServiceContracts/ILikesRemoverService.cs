using LibraryApp.Core.Domain.Entities;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for remove likes.
    /// </summary>
    public interface ILikesRemoverService
    {
        /// <summary>
        /// Removes like from the system.
        /// </summary>
        /// <param name="like">Like object that will be removed.</param>
        /// <returns>True if like was removed. Otherwise false.</returns>
        Task<bool> RemoveLike(Like like);
    }
}
