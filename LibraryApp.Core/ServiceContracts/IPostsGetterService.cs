using LibraryApp.Core.Domain.Entities;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for retrieving posts from db.
    /// </summary>
    public interface IPostsGetterService
    {
        /// <summary>
        /// Retrieves all posts from the system.
        /// </summary>
        /// <returns>List of Post objects.</returns>
        Task<List<Post>> GetAllPosts();

        /// <summary>
        /// Retrieves post from the system.
        /// </summary>
        /// <param name="postId">The id of post that will be retrieved.</param>
        /// <returns>Post object or null.</returns>
        Task<Post> GetPostByPostId(string postId);
    }
}
