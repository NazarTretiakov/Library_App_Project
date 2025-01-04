using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;

namespace LibraryApp.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Like entity.
    /// </summary>
    public interface ILikesRepository
    {
        /// <summary>
        /// Retrieves like from the data store.
        /// </summary>
        /// <param name="likeId">Id of the like that will be retrieved.</param>
        /// <returns>Like object or null.</returns>
        Task<Like> GetLike(string likeId);

        /// <summary>
        /// Retrieves like from the data store.
        /// </summary>
        /// <param name="userId">The id of user which liked the post.</param>
        /// <param name="postId">The id of post which was liked.</param>
        /// <returns>Like object or null.</returns>
        Task<Like> GetLike(string userId, string postId);

        /// <summary>
        /// Retrieves likes of post from the data store.
        /// </summary>
        /// <param name="postId">The id of post which likes will be retrieved.</param>
        /// <returns>List of Like objects or null.</returns>
        Task<List<Like>> GetPostLikes(string postId);

        /// <summary>
        /// Adds like to the data store.
        /// </summary>
        /// <param name="post">Post which was liked.</param>
        /// <param name="user">User that liked a post.</param>
        /// <returns>True if like was added. Otherwise false.</returns>
        Task<bool> AddLike(Post post, User user);

        /// <summary>
        /// Removes like from the data store.
        /// </summary>
        /// <param name="like">Like object which will be removed.</param>
        /// <returns>True if like was removed. Otherwise false.</returns>
        Task<bool> RemoveLike(Like like);
    }
}
