using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Repositories
{
    public class LikesRepository : ILikesRepository
    {
        private readonly LibraryDbContext _db;

        public LikesRepository(LibraryDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddLike(Post post, User user)
        {
            await _db.Likes.AddAsync(new Like() { LikeId = Guid.NewGuid(), User = user, UserId = user.Id, Post = post, PostId = post.PostId});

            var rowsAdded = await _db.SaveChangesAsync();

            return rowsAdded > 0;
        }

        public async Task<Like> GetLike(string likeId)
        {
            Like like = await _db.Likes.Include(l => l.User)
                                       .Include(l => l.Post)
                                       .FirstOrDefaultAsync(l => l.LikeId == Guid.Parse(likeId));

            return like;
        }

        public async Task<Like> GetLike(string userId, string postId)
        {
            Like like = await _db.Likes.Include(l => l.User)
                                       .Include(l => l.Post)
                                       .FirstOrDefaultAsync(l => l.UserId == Guid.Parse(userId) && l.PostId == Guid.Parse(postId));

            return like;
        }

        public async Task<List<Like>> GetPostLikes(string postId)
        {
            return await _db.Likes.Include(l => l.User)
                            .Include(l => l.Post)
                            .Where(l => l.PostId == Guid.Parse(postId))
                            .ToListAsync();
        }

        public async Task<bool> RemoveLike(Like like)
        {
            _db.Likes.Remove(like);

            int rowsDeleted = await _db.SaveChangesAsync();

            return rowsDeleted > 0;
        }
    }
}
