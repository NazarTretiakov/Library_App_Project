using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryApp.Infrastructure.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly LibraryDbContext _db;

        public PostsRepository(LibraryDbContext db)
        {
            _db = db;
        }

        public async Task<List<Post>> GetAllPosts()
        {
            return await _db.Posts.Include(p => p.User)
                                  .Include(p => p.Topics)
                                    .ThenInclude(pt => pt.Topic)
                                  .Include(p => p.Likes)
                                  .Include(p => p.Saves)
                                  .Include(p => p.Comments)
                                    .ThenInclude(c => c.User)
                                  .ToListAsync();
        }

        public async Task<Post> GetPost(string postId)
        {
            return await _db.Posts.Include(p => p.User)
                                  .Include(p => p.Topics)
                                    .ThenInclude(pt => pt.Topic)
                                  .Include(p => p.Likes)
                                  .Include(p => p.Saves)
                                  .Include(p => p.Comments)
                                    .ThenInclude(c => c.User)
                                  .FirstOrDefaultAsync(p => p.PostId == Guid.Parse(postId));
        }

        public async Task<bool> AddPost(Post post)
        {
            await _db.Posts.AddAsync(post);

            var rowsAdded = await _db.SaveChangesAsync();

            return rowsAdded > 0;
        }

        public async Task<List<Post>> GetFilteredPosts(Expression<Func<Post, bool>> predicate)
        {
            return await _db.Posts.Include(p => p.User)
                                  .Include(p => p.Topics)
                                    .ThenInclude(pt => pt.Topic)
                                  .Include(p => p.Likes)
                                  .Include(p => p.Saves)
                                  .Include(p => p.Comments)
                                    .ThenInclude(c => c.User)
                                  .Where(predicate)
                                  .ToListAsync();
        }

        public async Task<List<Post>> GetUserPosts(string userId)
        {
            return await _db.Posts.Include(p => p.User)
                      .Include(p => p.Topics)
                        .ThenInclude(pt => pt.Topic)
                      .Include(p => p.Likes)
                      .Include(p => p.Saves)
                      .Include(p => p.Comments)
                        .ThenInclude(c => c.User)
                      .Where(p => p.User.Id == Guid.Parse(userId))
                      .ToListAsync();
        }

        public async Task<List<Post>> GetFilteredUserPosts(string userId, Expression<Func<Post, bool>> predicate)
        {
            return await _db.Posts.Include(p => p.User)
                                 .Include(p => p.Topics)
                                   .ThenInclude(pt => pt.Topic)
                                 .Include(p => p.Likes)
                                 .Include(p => p.Saves)
                                 .Include(p => p.Comments)
                                   .ThenInclude(c => c.User)
                                 .Where(p => p.User.Id == Guid.Parse(userId))
                                 .Where(predicate)
                                 .ToListAsync();
        }
    }
}
