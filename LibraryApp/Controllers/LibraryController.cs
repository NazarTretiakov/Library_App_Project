using LibraryApp.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace LibraryApp.UI.Controllers
{
    public class LibraryController : Controller
    {
        private readonly UserManager<User> _userManager;

        public LibraryController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        [Route("/library")]
        public async Task<IActionResult> Index()
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            return View();
        }

        [Route("/library/book")]
        public async Task<IActionResult> Book()   //  TODO: Fix the bug where the old user profile image is not removed when the user updates their profile image.The old image should be removed.
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            return View();
        }
    }
}
