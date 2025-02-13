using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class SubscriptionsAdderService : ISubscriptionsAdderService
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly ISubscriptionsGetterService _subscriptionsGetterService;
        private readonly INotificationsCreatorService _notificationsCreatorService;

        public SubscriptionsAdderService(ISubscriptionsRepository subscriptionsRepository, ISubscriptionsGetterService subscriptionsGetterService, INotificationsCreatorService notificationsCreatorService)
        {
            _subscriptionsRepository = subscriptionsRepository;
            _subscriptionsGetterService = subscriptionsGetterService;
            _notificationsCreatorService = notificationsCreatorService;
        }

        public async Task<Subscription> AddSubscription(User user, User subscriber)
        {
            bool result = await _subscriptionsRepository.AddSubscription(user, subscriber);

            if (result)
            {
                Subscription subscription = await _subscriptionsGetterService.GetSubscriptionByUserIdAndSubscriberId(user.Id.ToString(), subscriber.Id.ToString());

                await _notificationsCreatorService.CreateNotification(new NotificationDTO()
                {
                    ObjectId = subscriber.Id.ToString(),
                    ReceiverId = user.Id.ToString(),
                    Content = $"{subscriber.UserName} has subscribed on your account."
                });

                return subscription;
            }
            else
            {
                throw new Exception("Error while adding subscription to db store.");
            }
        }
    }
}
