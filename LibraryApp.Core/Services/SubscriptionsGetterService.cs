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

        public async Task<List<Subscription>> GetUserFilteredSubscribers(string userId, string searchFilter, string searchString)
        {
            List<Subscription> filteredSubscribers = null;

            switch (searchFilter)
            {
                case "all":
                    filteredSubscribers = await _subscriptionsRepository.GetUserFilteredSubscribers(userId, s => s.Subscriber.NormalizedUserName.Contains(searchString.ToUpper()) ||
                                                                                                                   s.Subscriber.Firstname.ToUpper().Contains(searchString.ToUpper()) ||
                                                                                                                   s.Subscriber.Lastname.ToUpper().Contains(searchString.ToUpper()));
                break;

                case "username":
                    filteredSubscribers = await _subscriptionsRepository.GetUserFilteredSubscribers(userId, s => s.Subscriber.NormalizedUserName.Contains(searchString.ToUpper()));
                break;

                case "firstname":
                    filteredSubscribers = await _subscriptionsRepository.GetUserFilteredSubscribers(userId, s => s.Subscriber.Firstname.ToUpper().Contains(searchString.ToUpper()));
                break;

                case "lastname":
                    filteredSubscribers = await _subscriptionsRepository.GetUserFilteredSubscribers(userId, s => s.Subscriber.Lastname.ToUpper().Contains(searchString.ToUpper()));
                break;
            }

            return filteredSubscribers;
        }

        public async Task<List<Subscription>> GetUserFilteredSubscriptions(string userId, string searchFilter, string searchString)
        {
            List<Subscription> filteredSubscriptions = null;

            switch (searchFilter)
            {
                case "all":
                    filteredSubscriptions = await _subscriptionsRepository.GetUserFilteredSubscriptions(userId, s => s.User.NormalizedUserName.Contains(searchString.ToUpper()) ||
                                                                                                                   s.User.Firstname.ToUpper().Contains(searchString.ToUpper()) ||
                                                                                                                   s.User.Lastname.ToUpper().Contains(searchString.ToUpper()));
                    break;

                case "username":
                    filteredSubscriptions = await _subscriptionsRepository.GetUserFilteredSubscriptions(userId, s => s.User.NormalizedUserName.Contains(searchString.ToUpper()));
                    break;

                case "firstname":
                    filteredSubscriptions = await _subscriptionsRepository.GetUserFilteredSubscriptions(userId, s => s.User.Firstname.ToUpper().Contains(searchString.ToUpper()));
                    break;

                case "lastname":
                    filteredSubscriptions = await _subscriptionsRepository.GetUserFilteredSubscriptions(userId, s => s.User.Lastname.ToUpper().Contains(searchString.ToUpper()));
                    break;
            }

            return filteredSubscriptions;
        }
    }
}
