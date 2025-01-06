using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

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
                               .Where(c => c.User.Id == Guid.Parse(userId))
                               .ToListAsync();
        }

        public async Task<List<Comment>> GetCommentsOfPost(string postId)
        {
            return await _db.Comments.Include(c => c.User)
                                     .Include(c => c.Post)
                                     .Where(c => c.Post.PostId == Guid.Parse(postId))
                                     .ToListAsync();
        }
    }
}
