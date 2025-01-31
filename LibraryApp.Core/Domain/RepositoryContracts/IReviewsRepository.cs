using LibraryApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;

namespace LibraryApp.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Review entity.
    /// </summary>
    public interface IReviewsRepository
    {
        /// <summary>
        /// Retrieves all reviews of the book from the data store.
        /// </summary>
        /// <param name="bookId">The id of the book which revies will be retrieved.</param>
        /// <returns>List of Review objects or null</returns>
        Task<List<Review>> GetReviewsOfBook(string bookId);

        /// <summary>
        /// Retrieves all reviews that were made by particular user from the data store.
        /// </summary>
        /// <param name="userId">The id of the user which revies will be retrieved.</param>
        /// <returns>List of Review objects or null</returns>
        Task<List<Review>> GetReviewsByUser(string userId);

        /// <summary>
        /// Retrieves filtered reviews that were made by particular user from the data store.
        /// </summary>
        /// <param name="userId">The id of the user which revies will be retrieved.</param>
        /// <param name="predicate"> LINQ expression to filter reviews that will be retrieved.</param>
        /// <returns>List of Review objects or null</returns>
        Task<List<Review>> GetFilteredReviewsByUser(string userId, Expression<Func<Review, bool>> predicate);

        /// <summary>
        /// Adds review to the data store.
        /// </summary>
        /// <param name="review">Review object that will be added.</param>
        /// <returns>True if the review was added. Otherwise false.</returns>
        Task<bool> AddReview(Review review);
    }
}
