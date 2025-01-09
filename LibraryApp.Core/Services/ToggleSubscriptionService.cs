using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Core.Services
{
    public class ToggleSubscriptionService : IToggleSubscriptionService
    {
        private readonly IUsersGetterService _usersGetterService;
        private readonly ISubscriptionsGetterService _subscriptionsGetterService;
        private readonly ISubscriptionsRemoverService _subscriptionsRemoverService;
        private readonly ISubscriptionsAdderService _subscriptionsAdderService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public ToggleSubscriptionService(IUsersGetterService usersGetterService, ISubscriptionsGetterService subscriptionsGetterService, ISubscriptionsRemoverService subscriptionsRemoverService, ISubscriptionsAdderService subscriptionsAdderService, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _usersGetterService = usersGetterService;
            _subscriptionsGetterService = subscriptionsGetterService;
            _subscriptionsRemoverService = subscriptionsRemoverService;
            _subscriptionsAdderService = subscriptionsAdderService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<bool> ToggleSubscription(string userId)
        {
            User user = await _usersGetterService.GetUserByUserId(userId);

            if (user == null)
            {
                throw new ArgumentNullException();
            }

            User subscriber = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            Subscription subscriptionOfSubscriber = await _subscriptionsGetterService.GetSubscriptionByUserIdAndSubscriberId(user.Id.ToString(), subscriber.Id.ToString());

            if (subscriptionOfSubscriber != null)
            {
                await _subscriptionsRemoverService.RemoveSubscription(subscriptionOfSubscriber);

                return false;
            }
            else
            {
                await _subscriptionsAdderService.AddSubscription(user, subscriber);

                return true;
            }
        }
    }
}
