using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using LibraryApp.Core.DTO;

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
                                  .Include(u=> u.Subscribers)
                                    .ThenInclude(s => s.Subscriber)
                                  .Include(u => u.Subscriptions)
                                    .ThenInclude(s => s.User)
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
                                  .Include(u => u.Subscribers)
                                    .ThenInclude(s => s.Subscriber)
                                  .Include(u => u.Subscriptions)
                                    .ThenInclude(s => s.User)
                                  .FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));
        }

        public async Task<User> ChangeUserInformation(User user, ChangeProfileInformationDTO changeProfileInformationDTO)
        {
            user.Firstname = changeProfileInformationDTO.Firstname;
            user.Lastname = changeProfileInformationDTO.Lastname;
            user.Description = changeProfileInformationDTO.Description;

            await _db.SaveChangesAsync();

            return user;
        }

        public async Task<User> ChangeUserProfilePhoto(User user, string photoPath)
        {
            user.ProfilePhotoPath = photoPath;

            await _db.SaveChangesAsync();

            return user;
        }
    }
}
