using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Repositories
{
    public class TopicsRepository : ITopicsRepository
    {
        private readonly LibraryDbContext _db;

        public TopicsRepository(LibraryDbContext db)
        {
            _db = db;
        }

        public async Task<List<Topic>> GetAllTopics()
        {
            return await _db.Topics.Include(t => t.Posts).ToListAsync();
        }

        public async Task<Topic> GetTopic(string topicName)
        {
            return await _db.Topics.Include(t => t.Posts).FirstOrDefaultAsync(topic => topic.Name == topicName);
        }

        public async Task<bool> AddTopic(Topic topic)
        {
            await _db.Topics.AddAsync(topic);

            var entitiesAddedToDb = await _db.SaveChangesAsync();

            return entitiesAddedToDb > 0;
        }
    }
}
