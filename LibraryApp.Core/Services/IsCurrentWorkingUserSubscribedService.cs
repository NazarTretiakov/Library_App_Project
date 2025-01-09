using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Core.Services
{
    public class IsCurrentWorkingUserSubscribedService : IIsCurrentWorkingUserSubscribedService
    {
        private readonly ISubscriptionsGetterService _subscriptionsGetterService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public IsCurrentWorkingUserSubscribedService(ISubscriptionsGetterService subscriptionsGetterService, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _subscriptionsGetterService = subscriptionsGetterService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<bool> IsCurrentWorkingUserSubscribed(string userId)
        {
            User currentWorkingUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            Subscription subscription = await _subscriptionsGetterService.GetSubscriptionByUserIdAndSubscriberId(userId, currentWorkingUser.Id.ToString());

            return subscription != null;
        }
    }
}
