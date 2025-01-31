using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryApp.Infrastructure.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly LibraryDbContext _db;

        public CommentsRepository(LibraryDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddComment(Comment comment)
        {
            await _db.Comments.AddAsync(comment);

            var rowsAdded = await _db.SaveChangesAsync();

            return rowsAdded > 0;
        }

        public async Task<List<Comment>> GetCommentsByUser(string userId)
        {
            return await _db.Comments.Include(c => c.User)
                                     .Include(c => c.Post)
                                     .Include(c => c.Post.User)
                                     .Include(c => c.Post.Topics)
                                       .ThenInclude(pt => pt.Topic)
                                     .Where(c => c.User.Id == Guid.Parse(userId))
                                     .ToListAsync();
        }

        public async Task<List<Comment>> GetCommentsOfPost(string postId)
        {
            return await _db.Comments.Include(c => c.User)
                                     .Include(c => c.Post)
                                     .Include(c => c.Post.User)
                                     .Include(c => c.Post.Topics)
                                       .ThenInclude(pt => pt.Topic)
                                     .Where(c => c.Post.PostId == Guid.Parse(postId))
                                     .ToListAsync();
        }

        public async Task<List<Comment>> GetFilteredCommentsByUser(string userId, Expression<Func<Comment, bool>> predicate)
        {
            return await _db.Comments.Include(c => c.User)
                                     .Include(c => c.Post)
                                     .Include(c => c.Post.User)
                                     .Include(c => c.Post.Topics)
                                       .ThenInclude(pt => pt.Topic)
                                     .Where(c => c.User.Id == Guid.Parse(userId))
                                     .Where(predicate)
                                     .ToListAsync();
        }
    }
}
