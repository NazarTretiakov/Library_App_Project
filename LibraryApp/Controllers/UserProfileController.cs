using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.UI.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IUsersGetterService _usersGetterService;

        public UserProfileController(IUsersGetterService usersGetterService)
        {
            _usersGetterService = usersGetterService;
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

            return View(user);
        }

        [Route("/user-profile/subscribers")]
        public IActionResult Subscribers()
        {
            return View();
        }

        [Route("/user-profile/subscriptions")]
        public IActionResult Subscriptions()
        {
            return View();
        }
    }
}
