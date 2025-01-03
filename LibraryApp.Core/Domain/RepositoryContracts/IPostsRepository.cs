using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using System.Linq.Expressions;

namespace LibraryApp.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Post entity.
    /// </summary>
    public interface IPostsRepository
    {
        /// <summary>
        /// Retrieves all posts from the data store.
        /// </summary>
        /// <returns>The list of Post objects.</returns>
        Task<List<Post>> GetAllPosts();

        /// <summary>
        /// Retrieves post from the data store.
        /// </summary>
        /// <param name="postId">Id of the post that will be retrieved.</param>
        /// <returns>Post object or null.</returns>
        Task<Post> GetPost(string postId);

        /// <summary>
        /// Adds post to the data store.
        /// </summary>
        /// <param name="topic">Post object that will be added to the data store.</param>
        /// <returns>True if post was added. Otherwise false.</returns>
        Task<bool> AddPost(Post post);

        /// <summary>
        /// Retrieves filtered posts from the data store.
        /// </summary>
        /// <param name="predicate">LINQ expression to filter posts that will be retrieved.</param>
        /// <returns>List of Post objects or null.</returns>
        Task<List<Post>> GetFilteredPosts(Expression<Func<Post, bool>> predicate);
    }
}
