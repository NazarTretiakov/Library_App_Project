using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.DTO;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for creating new reviews.
    /// </summary>
    public interface IReviewsCreatorService
    {
        /// <summary>
        /// Adds a review by current working user to the system.
        /// </summary>
        /// <param name="reviewDTO">The data transfer object that contains information about the review that will be created.</param>
        /// <returns>The created object of the review.</returns>
        Task<Review> CreateReview(ReviewDTO reviewDTO);
    }
}
