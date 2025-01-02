using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.DTO;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for creating new posts.
    /// </summary>
    public interface IPostsCreatorService
    {
        /// <summary>
        /// Adds a post to the system.
        /// </summary>
        /// <param name="postDTO">The data transfer object that contains information about the post that will be added.</param>
        /// <returns>The created object of the post.</returns>
        Task<Post> CreatePost(PostDTO postDTO);
    }
}
