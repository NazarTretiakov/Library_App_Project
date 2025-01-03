using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;

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
    }
}
