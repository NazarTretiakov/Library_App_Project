using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class SubscriptionsGetterService : ISubscriptionsGetterService
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;

        public SubscriptionsGetterService(ISubscriptionsRepository subscriptionsRepository)
        {
            _subscriptionsRepository = subscriptionsRepository;
        }

        public async Task<List<Subscription>> GetUserSubscriptions(string userId)
        {
            return await _subscriptionsRepository.GetUserSubscriptions(userId);
        }

        public async Task<Subscription> GetSubscriptionByUserIdAndSubscriberId(string userId, string subscriberId)
        {
            return await _subscriptionsRepository.GetSubscription(userId, subscriberId);
        }

        public async Task<List<Subscription>> GetUserSubscribers(string userId)
        {
            return await _subscriptionsRepository.GetUserSubscribers(userId);
        }
    }
}
