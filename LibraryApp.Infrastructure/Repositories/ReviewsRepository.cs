using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryApp.Infrastructure.Repositories
{
    public class ReviewsRepository : IReviewsRepository
    {
        private readonly LibraryDbContext _db;

        public ReviewsRepository(LibraryDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddReview(Review review)
        {
            await _db.Reviews.AddAsync(review);

            var rowsAdded = await _db.SaveChangesAsync();

            return rowsAdded > 0;
        }

        public async Task<List<Review>> GetReviewsByUser(string userId)
        {
            return await _db.Reviews.Include(r => r.Book)
                                    .Include(r => r.User)
                                    .Where(c => c.User.Id == Guid.Parse(userId))
                                    .ToListAsync();
        }

        public async Task<List<Review>> GetReviewsOfBook(string bookId)
        {
            return await _db.Reviews.Include(r => r.Book)
                                    .Include(r => r.User)
                                    .Where(c => c.Book.BookId == Guid.Parse(bookId))
                                    .ToListAsync();
        }

        public async Task<List<Review>> GetFilteredReviewsByUser(string userId, Expression<Func<Review, bool>> predicate)
        {
            return await _db.Reviews.Include(r => r.Book)
                                    .Include(r => r.User)
                                    .Where(c => c.User.Id == Guid.Parse(userId))
                                    .Where(predicate)
                                    .ToListAsync();
        }
    }
}
