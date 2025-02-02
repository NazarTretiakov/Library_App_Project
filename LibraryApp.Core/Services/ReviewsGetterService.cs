using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class ReviewsGetterService : IReviewsGetterService
    {
        private readonly IReviewsRepository _reviewsRepository;

        public ReviewsGetterService(IReviewsRepository reviewsRepository)
        {
            _reviewsRepository = reviewsRepository;
        }

        public async Task<List<Review>> GetUserReviews(string userId)
        {
            return await _reviewsRepository.GetReviewsByUser(userId);
        }

        public async Task<List<Review>> GetUserFilteredReviews(string userId, string searchFilter, string searchString)
        {
            List<Review> filteredReviews = null;

            switch (searchFilter)
            {
                case "all":
                    filteredReviews = await _reviewsRepository.GetFilteredReviewsByUser(userId, r => r.Title.ToUpper().Contains(searchString.ToUpper()) ||
                                                                                                     r.Rating.ToString() == searchString);
                    break;

                case "title":
                    filteredReviews = await _reviewsRepository.GetFilteredReviewsByUser(userId, r => r.Title.ToUpper().Contains(searchString.ToUpper()));
                    break;

                case "rating":
                    filteredReviews = await _reviewsRepository.GetFilteredReviewsByUser(userId, r => r.Rating.ToString() == searchString);
                    break;
            }

            return filteredReviews;
        }
    }
}
