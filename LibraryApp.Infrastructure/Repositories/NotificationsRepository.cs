using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Repositories
{
    public class NotificationsRepository : INotificationsRepository
    {
        private readonly LibraryDbContext _db;

        public NotificationsRepository(LibraryDbContext db)
        {
            _db = db;
        }

        public async Task<List<Notification>> GetAllNotifications()
        {
            return await _db.Notifications.Include(n => n.Book)
                                          .Include(n => n.User)
                                          .Include(n => n.NotificationReceiver)
                                          .ToListAsync();
        }

        public async Task<Notification> GetNotification(string notificationId)
        {
            return await _db.Notifications.Include(n => n.Book)
                                          .Include(n => n.User)
                                          .Include(n => n.NotificationReceiver)
                                          .FirstOrDefaultAsync(n => n.NotificationId == Guid.Parse(notificationId));
        }

        public async Task<bool> CreateNotification(Notification notification)
        {
            await _db.Notifications.AddAsync(notification);

            var entitiesAddedToDb = await _db.SaveChangesAsync();

            return entitiesAddedToDb > 0;
        }

        public async Task<List<Notification>> GetUserNotifications(string userId)
        {
            return await _db.Notifications.Include(n => n.Book)
                                          .Include(n => n.User)
                                          .Include(n => n.NotificationReceiver)
                                          .Where(n => n.NotificationReceiver.Id == Guid.Parse(userId))
                                          .ToListAsync();
        }
    }
}
