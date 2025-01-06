using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.DTO;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for creating new comments.
    /// </summary>
    public interface ICommentsCreatorService
    {
        /// <summary>
        /// Adds a comment by current working user to the system.
        /// </summary>
        /// <param name="createCommentDTO">The data transfer object that contains information about the comment that will be created.</param>
        /// <returns>The created object of the comment.</returns>
        Task<Comment> CreateComment(CreateCommentDTO createCommentDTO);
    }
}
