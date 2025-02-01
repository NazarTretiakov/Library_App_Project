using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Repositories
{
    public class SavesRepository : ISavesRepository
    {
        private readonly LibraryDbContext _db;

        public SavesRepository(LibraryDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddBookSave(Book book, User user)
        {
            await _db.Saves.AddAsync(new Save() { SaveId = Guid.NewGuid(), User = user, UserId = user.Id, Book = book, BookId = book.BookId});

            var rowsAdded = await _db.SaveChangesAsync();
            
            return rowsAdded > 0;
        }

        public async Task<bool> AddPostSave(Post post, User user)
        {
            await _db.Saves.AddAsync(new Save() { SaveId = Guid.NewGuid(), User = user, UserId = user.Id, Post = post, PostId = post.PostId});

            var rowsAdded = await _db.SaveChangesAsync();
            
            return rowsAdded > 0;
        }

        public async Task<Save> GetSave(string saveId)
        {
            Save save = await _db.Saves.Include(s => s.User)
                                       .Include(s => s.Post)
                                       .Include(s => s.Book)
                                       .FirstOrDefaultAsync(l => l.SaveId == Guid.Parse(saveId));

            return save;
        }

        public async Task<Save> GetSave(string userId, string objectId)
        {
            Save save = await _db.Saves.Include(s => s.User)
                                       .Include(s => s.Post)
                                       .Include(s => s.Book)
                                       .FirstOrDefaultAsync(s => s.UserId == Guid.Parse(userId) && (s.PostId == Guid.Parse(objectId) || s.BookId == Guid.Parse(objectId)));

            return save;
        }

        public async Task<List<Save>> GetSavesByObjectId(string objectId)
        {
            return await _db.Saves.Include(s => s.User)
                                  .Include(s => s.Post)
                                  .Include(s => s.Book)
                                  .Where(s => s.PostId == Guid.Parse(objectId) || s.BookId == Guid.Parse(objectId))
                                  .ToListAsync();
        }

        public async Task<List<Save>> GetSavesByUserId(string userId)
        {
            return await _db.Saves.Include(s => s.User)
                                  .Include(s => s.Post)
                                    .ThenInclude(p => p.User)
                                  .Include(s => s.Post.Topics)
                                    .ThenInclude(pt => pt.Topic)
                                  .Include(s => s.Book)
                                    .ThenInclude(b => b.Author)
                                  .Include(s => s.Book.Genres)
                                    .ThenInclude(bg => bg.Genre)
                                  .Where(s => s.UserId == Guid.Parse(userId))
                                  .ToListAsync();
        }

        public async Task<bool> RemoveSave(Save save)
        {
            _db.Saves.Remove(save);

            int rowsDeleted = await _db.SaveChangesAsync();

            return rowsDeleted > 0;
        }
    }
}
