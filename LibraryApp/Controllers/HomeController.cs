using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;

        public HomeController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [Route("/")]
        [Route("/home")]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            return View();
        }

        [Route("/home/about-us")]
        [AllowAnonymous]
        public IActionResult AboutUs()
        {
            return View();
        }

        [Route("/home/ask-us")]
        [AllowAnonymous]
        public IActionResult AskUs()
        {
            return View();
        }
    }
}
