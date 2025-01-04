using LibraryApp.Core.Domain.Entities;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for toggling like.
    /// </summary>
    public interface IToggleLikeService
    {
        /// <summary>
        /// Toggles like of the post for current user in the system.
        /// </summary>
        /// <param name="postId">The id of the post which like will be toggled.</param>
        /// <returns>True if the like is active, false if like is inactive.</returns>
        Task<bool> ToggleLike(string postId);
    }
}
