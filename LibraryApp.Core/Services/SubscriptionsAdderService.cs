using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class SubscriptionsAdderService : ISubscriptionsAdderService
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly ISubscriptionsGetterService _subscriptionsGetterService;

        public SubscriptionsAdderService(ISubscriptionsRepository subscriptionsRepository, ISubscriptionsGetterService subscriptionsGetterService)
        {
            _subscriptionsRepository = subscriptionsRepository;
            _subscriptionsGetterService = subscriptionsGetterService;
        }

        public async Task<Subscription> AddSubscription(User user, User subscriber)
        {
            bool result = await _subscriptionsRepository.AddSubscription(user, subscriber);

            if (result)
            {
                return await _subscriptionsGetterService.GetSubscriptionByUserIdAndSubscriberId(user.Id.ToString(), subscriber.Id.ToString());
            }
            else
            {
                throw new Exception("Error while adding subscription to db store.");
            }
        }
    }
}
