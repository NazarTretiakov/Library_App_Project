using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ServiceContracts;
using LibraryApp.Core.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.UI.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IUsersGetterService _usersGetterService;
        private readonly IToggleSubscriptionService _toggleSubscriptionService;
        private readonly ISubscriptionsGetterService _subscriptionsGetterService;
        private readonly IIsCurrentWorkingUserSubscribedService _isCurrentWorkingUserSubscribed;

        public UserProfileController(IUsersGetterService usersGetterService, IToggleSubscriptionService toggleSubscriptionService, ISubscriptionsGetterService subscriptionsGetterService, IIsCurrentWorkingUserSubscribedService isCurrentWorkingUserSubscribed)
        {
            _usersGetterService = usersGetterService;
            _toggleSubscriptionService = toggleSubscriptionService;
            _subscriptionsGetterService = subscriptionsGetterService;
            _isCurrentWorkingUserSubscribed = isCurrentWorkingUserSubscribed;
        }

        [Route("/user-profile")]
        public IActionResult Index()
        {
            return RedirectToAction("Posts");
        }

        [Route("/user-profile/posts")]
        public async Task<IActionResult> Posts(string userId)
        {
            User user = await _usersGetterService.GetUserByUserId(userId);

            ViewBag.IsSubscribed = await _isCurrentWorkingUserSubscribed.IsCurrentWorkingUserSubscribed(userId);

            return View(user);  //TODO: create empty state of the page, for situation when user has no posts
        }

        [Route("/user-profile/toggle-subscription")]
        [HttpPost]
        public async Task<IActionResult> ToggleSubscription([FromBody] ToggleSubscriptionDTO toggleSubscriptionDTO)
        {
            bool isSubscribed = await _toggleSubscriptionService.ToggleSubscription(toggleSubscriptionDTO.UserId);
            User user = await _usersGetterService.GetUserByUserId(toggleSubscriptionDTO.UserId);

            ViewBag.IsSubscribed = isSubscribed;

            return PartialView("_SubscribeButton", user);
        }

        [Route("/user-profile/subscribers")]
        public IActionResult Subscribers()
        {
            return View();  //TODO: create empty state of the page, for situation when user has no subscribers
        }

        [Route("/user-profile/subscriptions")]
        public IActionResult Subscriptions()
        {
            return View();  //TODO: create empty state of the page, for situation when user has no subscriptions
        }
    }
}
