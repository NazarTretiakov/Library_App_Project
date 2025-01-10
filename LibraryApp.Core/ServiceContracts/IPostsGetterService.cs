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

        /// <summary>
        /// Retrieves filtered posts from the system.
        /// </summary>
        /// <param name="searchString">The phrase to be searched for.</param>
        /// <param name="searchFilter">The name of the filter that will be used to search.</param>
        /// <returns>The list of Post objects or null.</returns>
        Task<List<Post>> GetFilteredPosts(string searchFilter, string searchString);

        /// <summary>
        /// Retrieves all user's posts from the system.
        /// </summary>
        /// <param name="userId">The id of the user which posts will be retrieved.</param>
        /// <returns>The list of Post objects or null.</returns>
        Task<List<Post>> GetUserPosts(string userId);

        /// <summary>
        /// Retrieves filtered posts of particular user from the system.
        /// </summary>
        /// <param name="userId">The id of the user which posts will be retrieved.</param>
        /// <param name="searchString">The phrase to be searched for.</param>
        /// <param name="searchFilter">The name of the filter that will be used to search.</param>
        /// <returns>The list of Post objects or null.</returns>
        Task<List<Post>> GetFilteredUserPosts(string userId, string searchFilter, string searchString);
    }
}
