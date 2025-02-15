using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Repositories
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly LibraryDbContext _db;

        public MessagesRepository(LibraryDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateMessage(Message message)
        {
            await _db.Messages.AddAsync(message);

            var entitiesAddedToDb = await _db.SaveChangesAsync();

            return entitiesAddedToDb > 0;
        }

        public async Task<List<Message>> GetAllMessages()
        {
            return await _db.Messages.Include(m => m.User)
                                     .ToListAsync();
        }

        
    }
}
