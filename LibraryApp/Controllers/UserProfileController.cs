using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.UI.Controllers
{
    [Authorize(Roles = "User")]
    public class UserProfileController : Controller
    {
        private readonly IUsersGetterService _usersGetterService;
        private readonly IToggleSubscriptionService _toggleSubscriptionService;
        private readonly ISubscriptionsGetterService _subscriptionsGetterService;
        private readonly IIsCurrentWorkingUserSubscribedService _isCurrentWorkingUserSubscribed;
        private readonly IPostsGetterService _postsGetterService;
        private readonly ICommentsGetterService _commentsGetterService;
        private readonly IReviewsGetterService _reviewsGetterService;
        private readonly UserManager<User> _userManager;

        public UserProfileController(IUsersGetterService usersGetterService, IToggleSubscriptionService toggleSubscriptionService, ISubscriptionsGetterService subscriptionsGetterService, IIsCurrentWorkingUserSubscribedService isCurrentWorkingUserSubscribed, IPostsGetterService postsGetterService, ICommentsGetterService commentsGetterService, IReviewsGetterService reviewsGetterService, UserManager<User> userManager)
        {
            _usersGetterService = usersGetterService;
            _toggleSubscriptionService = toggleSubscriptionService;
            _subscriptionsGetterService = subscriptionsGetterService;
            _isCurrentWorkingUserSubscribed = isCurrentWorkingUserSubscribed;
            _postsGetterService = postsGetterService;
            _commentsGetterService = commentsGetterService;
            _reviewsGetterService = reviewsGetterService;
            _userManager = userManager;
        }

        [Route("/user-profile")]
        public IActionResult Index(string userId)
        {
            if (!Guid.TryParse(userId, out Guid result))
            {
                return NotFound(); 
            }

            return RedirectToAction(nameof(UserProfileController.Posts), "UserProfile", new { userId = userId});
        }

        [Route("/user-profile/posts")]
        public async Task<IActionResult> Posts(string userId, string searchString, string searchFilter = "all")
        {
            if (!Guid.TryParse(userId, out Guid result))
            {
                return NotFound(); 
            }

            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

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

            ViewBag.Posts = posts.OrderByDescending(p => p.DateOfPublication).ToList();

            return View(user);
        }

        [Route("/user-profile/replies")]
        public async Task<IActionResult> Comments(string userId, string searchString, string searchFilter = "all")
        {
            if (!Guid.TryParse(userId, out Guid result))
            {
                return NotFound();
            }

            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            User user = await _usersGetterService.GetUserByUserId(userId);
            List<Comment> comments;

            ViewBag.IsSubscribed = await _isCurrentWorkingUserSubscribed.IsCurrentWorkingUserSubscribed(userId);
            ViewBag.SearchString = searchString;
            ViewBag.SearchFilter = searchFilter;

            if (searchString == null)
            {
                comments = await _commentsGetterService.GetUserComments(userId);
            }
            else
            {
                comments = await _commentsGetterService.GetUserFilteredComments(userId, searchFilter, searchString);
            }

            ViewBag.Comments = comments.OrderByDescending(p => p.DateOfPublication).ToList();

            return View(user);
        }

        [Route("/user-profile/reviews")]
        public async Task<IActionResult> Reviews(string userId, string searchString, string searchFilter = "all")
        {
            if (!Guid.TryParse(userId, out Guid result))
            {
                return NotFound();
            }

            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            User user = await _usersGetterService.GetUserByUserId(userId);
            List<Review> reviews;

            ViewBag.IsSubscribed = await _isCurrentWorkingUserSubscribed.IsCurrentWorkingUserSubscribed(userId);
            ViewBag.SearchString = searchString;
            ViewBag.SearchFilter = searchFilter;

            if (searchString == null)
            {
                reviews = await _reviewsGetterService.GetUserReviews(userId);
            }
            else
            {
                reviews = await _reviewsGetterService.GetUserFilteredReviews(userId, searchFilter, searchString);
            }

            ViewBag.Reviews = reviews.OrderByDescending(p => p.DateOfPublication).ToList();

            return View(user);
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
            if (!Guid.TryParse(userId, out Guid result))
            {
                return NotFound();
            }

            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

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

            return View(user);
        }

        [Route("/user-profile/subscriptions")]
        public async Task<IActionResult> Subscriptions(string userId, string searchString, string searchFilter = "all")
        {
            if (!Guid.TryParse(userId, out Guid result))
            {
                return NotFound();
            }

            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

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

            return View(user);
        }
    }
}
