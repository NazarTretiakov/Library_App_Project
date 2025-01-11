using LibraryApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;

namespace LibraryApp.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Comment entity.
    /// </summary>
    public interface ICommentsRepository
    {
        /// <summary>
        /// Retrieves all comments of the post from the data store.
        /// </summary>
        /// <param name="postId">The id of the post which commentaries will be retrieved.</param>
        /// <returns>List of Comment objects or null</returns>
        Task<List<Comment>> GetCommentsOfPost(string postId);

        /// <summary>
        /// Retrieves all comments that were made by particular user from the data store.
        /// </summary>
        /// <param name="userId">The id of the user which commentaries will be retrieved.</param>
        /// <returns>List of Comment objects or null</returns>
        Task<List<Comment>> GetCommentsByUser(string userId);

        /// <summary>
        /// Retrieves filtered comments that were made by particular user form the data store.
        /// </summary>
        /// <param name="userId">The id of the user which commentaries will be retrieved.</param>
        /// <param name="predicate"> LINQ expression to filter posts that will be retrieved.</param>
        /// <returns>List of Comment objects or null</returns>
        Task<List<Comment>> GetFilteredCommentsByuser(string userId, Expression<Func<Comment, bool>> predicate);

        /// <summary>
        /// Adds comment to the data store.
        /// </summary>
        /// <param name="comment">Comment object that will be added.</param>
        /// <returns>True if the comment was added. Otherwise false.</returns>
        Task<bool> AddComment(Comment comment);
    }
}
