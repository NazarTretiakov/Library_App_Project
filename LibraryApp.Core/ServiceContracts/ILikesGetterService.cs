using LibraryApp.Core.Domain.Entities;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for retrieving likes from db.
    /// </summary>
    public interface ILikesGetterService
    {
        /// <summary>
        /// Retrieves like from the system.
        /// </summary>
        /// <param name="userId">The id of user which liked the post.</param>
        /// <param name="postId">The id of post which was liked.</param>
        /// <returns>Like object or null.</returns>
        Task<Like> GetLikeByUserIdAndPostId(string userId, string postId);

        /// <summary>
        /// Retrieves likes of post from the system.
        /// </summary>
        /// <param name="postId">The id of post which likes will be retrieved.</param>
        /// <returns>List of Like objects or null.</returns>
        Task<List<Like>> GetPostLikes(string postId);
    }
}
