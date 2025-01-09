using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;

namespace LibraryApp.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly LibraryDbContext _db;

        public UsersRepository(LibraryDbContext db)
        {
            _db = db;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _db.Users.Include(u => u.Posts)
                                    .ThenInclude(p => p.Topics)
                                      .ThenInclude(pt => pt.Topic)
                                  .Include(u => u.Likes)
                                  .Include(u => u.Saves)
                                  .Include(u => u.Comments)
                                  .Include(u => u.Subscribers)
                                  .Include(u => u.Subscriptions)
                                  .ToListAsync();
        }

        public async Task<User> GetUser(string userId)
        {
            return await _db.Users.Include(u => u.Posts)
                                    .ThenInclude(p => p.Topics)
                                      .ThenInclude(pt => pt.Topic)
                                  .Include(u => u.Likes)
                                  .Include(u => u.Saves)
                                  .Include(u => u.Comments)
                                  .Include(u => u.Subscribers)
                                  .Include(u => u.Subscriptions)
                                  .FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));
        }
    }
}
