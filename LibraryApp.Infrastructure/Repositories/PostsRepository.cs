using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

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
                                  .FirstOrDefaultAsync(p => p.PostId == Guid.Parse(postId));
        }

        public async Task<bool> AddPost(Post post)
        {
            await _db.Posts.AddAsync(post);

            var entitiesAddedToDb = await _db.SaveChangesAsync();

            return entitiesAddedToDb > 0;
        }
    }
}
