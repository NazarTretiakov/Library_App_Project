using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace LibraryApp.Infrastructure.Repositories
{
    public class SubscriptionsRepository : ISubscriptionsRepository
    {
        private readonly LibraryDbContext _db;

        public SubscriptionsRepository(LibraryDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddSubscription(User user, User subscriber)
        {
            await _db.Subscriptions.AddAsync(new Subscription() { SubscriptionId = Guid.NewGuid(), UserId = user.Id, User = user, SubscriberId = subscriber.Id, Subscriber = subscriber});

            var rowsAdded = await _db.SaveChangesAsync();

            return rowsAdded > 0;
        }

        public async Task<List<Subscription>> GetUserSubscriptions(string userId)
        {
            List<Subscription> subscriptions = await _db.Subscriptions.Include(s => s.User)
                                                                        .ThenInclude(u => u.Posts)
                                                                          .ThenInclude(p => p.Topics)
                                                                            .ThenInclude(pt => pt.Topic)
                                                                      .Include(s => s.User.Likes)
                                                                      .Include(s => s.User.Saves)
                                                                      .Include(s => s.User.Comments)
                                                                      .Include(s => s.User.Subscribers)
                                                                      .Include(s => s.User.Subscriptions)
                                                                      .Include(s => s.Subscriber)
                                                                        .ThenInclude(u => u.Posts)
                                                                          .ThenInclude(p => p.Topics)
                                                                            .ThenInclude(pt => pt.Topic)
                                                                      .Include(s => s.Subscriber.Likes)
                                                                      .Include(s => s.Subscriber.Saves)
                                                                      .Include(s => s.Subscriber.Comments)
                                                                      .Include(s => s.Subscriber.Subscribers)
                                                                      .Include(s => s.Subscriber.Subscriptions)
                                                                      .Where(s => s.SubscriberId == Guid.Parse(userId))
                                                                      .ToListAsync();

            return subscriptions;
        }

        public async Task<Subscription> GetSubscription(string subscriptionId)
        {
            Subscription subscription = await _db.Subscriptions.Include(s => s.User)
                                                                 .ThenInclude(u => u.Posts)
                                                                   .ThenInclude(p => p.Topics)
                                                                     .ThenInclude(pt => pt.Topic)
                                                               .Include(s => s.User.Likes)
                                                               .Include(s => s.User.Saves)
                                                               .Include(s => s.User.Comments)
                                                               .Include(s => s.User.Subscribers)
                                                               .Include(s => s.User.Subscriptions)
                                                               .Include(s => s.Subscriber)
                                                                 .ThenInclude(u => u.Posts)
                                                                   .ThenInclude(p => p.Topics)
                                                                     .ThenInclude(pt => pt.Topic)
                                                               .Include(s => s.Subscriber.Likes)
                                                               .Include(s => s.Subscriber.Saves)
                                                               .Include(s => s.Subscriber.Comments)
                                                               .Include(s => s.Subscriber.Subscribers)
                                                               .Include(s => s.Subscriber.Subscriptions)
                                                               .FirstOrDefaultAsync(s => s.SubscriptionId == Guid.Parse(subscriptionId));

            return subscription;
        }

        public async Task<Subscription> GetSubscription(string userId, string subscriberId)
        {
            Subscription subscription = await _db.Subscriptions.Include(s => s.User)
                                                     .ThenInclude(u => u.Posts)
                                                       .ThenInclude(p => p.Topics)
                                                         .ThenInclude(pt => pt.Topic)
                                                   .Include(s => s.User.Likes)
                                                   .Include(s => s.User.Saves)
                                                   .Include(s => s.User.Comments)
                                                   .Include(s => s.User.Subscribers)
                                                   .Include(s => s.User.Subscriptions)
                                                   .Include(s => s.Subscriber)
                                                     .ThenInclude(u => u.Posts)
                                                       .ThenInclude(p => p.Topics)
                                                         .ThenInclude(pt => pt.Topic)
                                                   .Include(s => s.Subscriber.Likes)
                                                   .Include(s => s.Subscriber.Saves)
                                                   .Include(s => s.Subscriber.Comments)
                                                   .Include(s => s.Subscriber.Subscribers)
                                                   .Include(s => s.Subscriber.Subscriptions)
                                                   .FirstOrDefaultAsync(s => s.UserId == Guid.Parse(userId) && s.SubscriberId == Guid.Parse(subscriberId));

            return subscription;
        }

        public async Task<List<Subscription>> GetUserSubscribers(string userId)
        {
            List<Subscription> subscriptions = await _db.Subscriptions.Include(s => s.User)
                                                            .ThenInclude(u => u.Posts)
                                                              .ThenInclude(p => p.Topics)
                                                                .ThenInclude(pt => pt.Topic)
                                                          .Include(s => s.User.Likes)
                                                          .Include(s => s.User.Saves)
                                                          .Include(s => s.User.Comments)
                                                          .Include(s => s.User.Subscribers)
                                                          .Include(s => s.User.Subscriptions)
                                                          .Include(s => s.Subscriber)
                                                            .ThenInclude(u => u.Posts)
                                                              .ThenInclude(p => p.Topics)
                                                                .ThenInclude(pt => pt.Topic)
                                                          .Include(s => s.Subscriber.Likes)
                                                          .Include(s => s.Subscriber.Saves)
                                                          .Include(s => s.Subscriber.Comments)
                                                          .Include(s => s.Subscriber.Subscribers)
                                                          .Include(s => s.Subscriber.Subscriptions)
                                                          .Where(s => s.UserId == Guid.Parse(userId))
                                                          .ToListAsync();

            return subscriptions;
        }

        public async Task<bool> RemoveSubscription(Subscription subscription)
        {
            _db.Subscriptions.Remove(subscription);

            int rowsDeleted = await _db.SaveChangesAsync();

            return rowsDeleted > 0;
        }

        public async Task<List<Subscription>> GetUserFilteredSubscribers(string userId, Expression<Func<Subscription, bool>> predicate)
        {
            List<Subscription> subscriptions = await _db.Subscriptions.Include(s => s.User)
                                                                        .ThenInclude(u => u.Posts)
                                                                          .ThenInclude(p => p.Topics)
                                                                            .ThenInclude(pt => pt.Topic)
                                                                      .Include(s => s.User.Likes)
                                                                      .Include(s => s.User.Saves)
                                                                      .Include(s => s.User.Comments)
                                                                      .Include(s => s.User.Subscribers)
                                                                      .Include(s => s.User.Subscriptions)
                                                                      .Include(s => s.Subscriber)
                                                                        .ThenInclude(u => u.Posts)
                                                                          .ThenInclude(p => p.Topics)
                                                                            .ThenInclude(pt => pt.Topic)
                                                                      .Include(s => s.Subscriber.Likes)
                                                                      .Include(s => s.Subscriber.Saves)
                                                                      .Include(s => s.Subscriber.Comments)
                                                                      .Include(s => s.Subscriber.Subscribers)
                                                                      .Include(s => s.Subscriber.Subscriptions)
                                                                      .Where(s => s.UserId == Guid.Parse(userId))
                                                                      .Where(predicate)
                                                                      .ToListAsync();

            return subscriptions;
        }

        public async Task<List<Subscription>> GetUserFilteredSubscriptions(string userId, Expression<Func<Subscription, bool>> predicate)
        {
            List<Subscription> subscriptions = await _db.Subscriptions.Include(s => s.User)
                                                                       .ThenInclude(u => u.Posts)
                                                                         .ThenInclude(p => p.Topics)
                                                                           .ThenInclude(pt => pt.Topic)
                                                                     .Include(s => s.User.Likes)
                                                                     .Include(s => s.User.Saves)
                                                                     .Include(s => s.User.Comments)
                                                                     .Include(s => s.User.Subscribers)
                                                                     .Include(s => s.User.Subscriptions)
                                                                     .Include(s => s.Subscriber)
                                                                       .ThenInclude(u => u.Posts)
                                                                         .ThenInclude(p => p.Topics)
                                                                           .ThenInclude(pt => pt.Topic)
                                                                     .Include(s => s.Subscriber.Likes)
                                                                     .Include(s => s.Subscriber.Saves)
                                                                     .Include(s => s.Subscriber.Comments)
                                                                     .Include(s => s.Subscriber.Subscribers)
                                                                     .Include(s => s.Subscriber.Subscriptions)
                                                                     .Where(s => s.SubscriberId == Guid.Parse(userId))
                                                                     .Where(predicate)
                                                                     .ToListAsync();

            return subscriptions;
        }
    }
}
