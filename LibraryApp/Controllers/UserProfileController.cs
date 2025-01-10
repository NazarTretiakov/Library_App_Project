using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ServiceContracts;
using LibraryApp.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace LibraryApp.UI.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IUsersGetterService _usersGetterService;
        private readonly IToggleSubscriptionService _toggleSubscriptionService;
        private readonly ISubscriptionsGetterService _subscriptionsGetterService;
        private readonly IIsCurrentWorkingUserSubscribedService _isCurrentWorkingUserSubscribed;
        private readonly IPostsGetterService _postsGetterService;

        public UserProfileController(IUsersGetterService usersGetterService, IToggleSubscriptionService toggleSubscriptionService, ISubscriptionsGetterService subscriptionsGetterService, IIsCurrentWorkingUserSubscribedService isCurrentWorkingUserSubscribed, IPostsGetterService postsGetterService)
        {
            _usersGetterService = usersGetterService;
            _toggleSubscriptionService = toggleSubscriptionService;
            _subscriptionsGetterService = subscriptionsGetterService;
            _isCurrentWorkingUserSubscribed = isCurrentWorkingUserSubscribed;
            _postsGetterService = postsGetterService;
        }

        [Route("/user-profile")]
        public IActionResult Index()
        {
            return RedirectToAction("Posts");
        }

        [Route("/user-profile/posts")]
        public async Task<IActionResult> Posts(string userId, string searchString, string searchFilter = "all")
        {
            User user = await _usersGetterService.GetUserByUserId(userId);
            List<Post> posts;

            ViewBag.IsSubscribed = await _isCurrentWorkingUserSubscribed.IsCurrentWorkingUserSubscribed(userId);
            ViewBag.SearchString = searchString;
            ViewBag.SearchFilter = searchFilter;

            if (searchString == null)
            {
                posts = await _postsGetterService.GetUserPosts(userId);
            }
            else
            {
                posts = await _postsGetterService.GetFilteredUserPosts(userId, searchFilter, searchString);
            }

            ViewBag.Posts = posts.OrderByDescending(p => p.DateOfPublication).ToList();  //TODO: find out why I can't use here async version of query methods

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
        public async Task<IActionResult> Subscribers(string userId, string searchString, string searchFilter = "all")
        {
            User user = await _usersGetterService.GetUserByUserId(userId);
            List<Subscription> subscribers;

            ViewBag.SearchString = searchString;
            ViewBag.SearchFilter = searchFilter;

            if (searchString == null)
            {
                subscribers = await _subscriptionsGetterService.GetUserSubscribers(userId);
            }
            else
            {
                subscribers = await _subscriptionsGetterService.GetUserFilteredSubscribers(userId, searchFilter, searchString);
            }

            ViewBag.Subscribers = subscribers;

            return View(user);  //TODO: create empty state of the page, for situation when user has no subscribers
        }

        [Route("/user-profile/subscriptions")]
        public async Task<IActionResult> Subscriptions(string userId, string searchString, string searchFilter = "all")
        {
            User user = await _usersGetterService.GetUserByUserId(userId);
            List<Subscription> subscriptions;

            ViewBag.SearchString = searchString;
            ViewBag.SearchFilter = searchFilter;

            if (searchString == null)
            {
                subscriptions = await _subscriptionsGetterService.GetUserSubscriptions(userId);
            }
            else
            {
                subscriptions = await _subscriptionsGetterService.GetUserFilteredSubscriptions(userId, searchFilter, searchString);
            }

            ViewBag.Subscriptions = subscriptions;

            return View(user);  //TODO: create empty state of the page, for situation when user has no subscriptions
        }
    }
}
