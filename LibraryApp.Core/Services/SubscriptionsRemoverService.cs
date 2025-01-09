using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class SubscriptionsRemoverService : ISubscriptionsRemoverService
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;

        public SubscriptionsRemoverService(ISubscriptionsRepository subscriptionsRepository)
        {
            _subscriptionsRepository = subscriptionsRepository;
        }

        public async Task<bool> RemoveSubscription(Subscription subscription)
        {
            return await _subscriptionsRepository.RemoveSubscription(subscription);
        }
    }
}
