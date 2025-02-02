using LibraryApp.Core.Domain.Entities;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for retrieving reviews from db.
    /// </summary>
    public interface IReviewsGetterService
    {
        /// <summary>
        /// Retrieves all reviews that were made by particular user from the system.
        /// </summary>
        /// <param name="userId">The id of the user which reviews will be retrieved.</param>
        /// <returns>List of Review objects or null</returns>
        Task<List<Review>> GetUserReviews(string userId);

        /// <summary>
        /// Retrieves filtered reviews that were made by particular user form the system.
        /// </summary>
        /// <param name="userId">The id of the user which reviews will be retrieved.</param>
        /// <param name="searchFilter">The name of the filter that will be used to search.</param>
        /// <param name="searchString">The phrase to be searched for.</param>
        /// <returns>List of Review objects or null</returns>
        Task<List<Review>> GetUserFilteredReviews(string userId, string searchFilter, string searchString);
    }
}
