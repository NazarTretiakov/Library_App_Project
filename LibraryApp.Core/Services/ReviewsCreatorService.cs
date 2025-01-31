using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Core.Services
{
    public class ReviewsCreatorService : IReviewsCreatorService
    {
        private readonly IReviewsRepository _reviewsRepository;
        private readonly IBooksGetterService _booksGetterService;
        private readonly IBooksRepository _booksRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public ReviewsCreatorService(IReviewsRepository reviewsRepository, IBooksGetterService booksGetterService, IHttpContextAccessor httpContextAccessor, IBooksRepository booksRepository, UserManager<User> userManager)
        {
            _reviewsRepository = reviewsRepository;
            _booksGetterService = booksGetterService;
            _httpContextAccessor = httpContextAccessor;
            _booksRepository = booksRepository;
            _userManager = userManager;
        }

        public async Task<Review> CreateReview(ReviewDTO reviewDTO)
        {
            User currentWorkingUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            Book book = await _booksGetterService.GetBookByBookId(reviewDTO.BookId);

            Review newReview = new Review()
            {
                ReviewId = Guid.NewGuid(),
                User = currentWorkingUser,
                Book = book,
                Title = reviewDTO.Title,
                Rating = (byte)reviewDTO.Rating,
                Content = reviewDTO.Content,
                DateOfPublication = DateTime.Now
            };

            await _reviewsRepository.AddReview(newReview);

            await _booksRepository.UpdateRating(book);

            return newReview;
        }
    }
}
