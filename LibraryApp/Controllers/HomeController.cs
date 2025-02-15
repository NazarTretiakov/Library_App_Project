using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMessagesCreatorService _messagesCreatorService;

        public HomeController(UserManager<User> userManager, IMessagesCreatorService messagesCreatorService)
        {
            _userManager = userManager;
            _messagesCreatorService = messagesCreatorService;
        }

        [Route("/")]
        [AllowAnonymous]
        public async Task<IActionResult> Default()
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            if (currentWorkingUser == null || await _userManager.IsInRoleAsync(currentWorkingUser, "User"))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            if (await _userManager.IsInRoleAsync(currentWorkingUser, "Librarian"))
            {
                return RedirectToAction("Index", "Librarian", new { area = "Librarian" });
            }
            else
            {
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
            }
        }

        [Route("/home")]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            if (currentWorkingUser != null)
            {
                if (await _userManager.IsInRoleAsync(currentWorkingUser, "Librarian"))
                {
                    return RedirectToAction("Index", "Librarian", new { area = "Librarian" });
                }
                else if (await _userManager.IsInRoleAsync(currentWorkingUser, "Admin"))
                {
                    return RedirectToAction("Index", "Admin", new { area = "Admin" });
                }
            }

            return View();
        }

        [Route("/home/about-us")]
        [AllowAnonymous]
        public async Task<IActionResult> AboutUs()
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            if (currentWorkingUser != null)
            {
                if (await _userManager.IsInRoleAsync(currentWorkingUser, "Librarian"))
                {
                    return RedirectToAction("Index", "Librarian", new { area = "Librarian" });
                }
                else if (await _userManager.IsInRoleAsync(currentWorkingUser, "Admin"))
                {
                    return RedirectToAction("Index", "Admin", new { area = "Admin" });
                }
            }
            return View();
        }

        [Route("/home/ask-us")]
        [HttpGet]
        public async Task<IActionResult> AskUs()
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            if (currentWorkingUser != null)
            {
                if (await _userManager.IsInRoleAsync(currentWorkingUser, "Librarian"))
                {
                    return RedirectToAction("Index", "Librarian", new { area = "Librarian" });
                }
                else if (await _userManager.IsInRoleAsync(currentWorkingUser, "Admin"))
                {
                    return RedirectToAction("Index", "Admin", new { area = "Admin" });
                }
            }
            return View();
        }

        [Route("/home/ask-us")]
        [HttpPost]
        public async Task<IActionResult> AskUs(MessageDTO messageDTO)
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            if (currentWorkingUser != null)
            {
                if (await _userManager.IsInRoleAsync(currentWorkingUser, "Librarian"))
                {
                    return RedirectToAction("Index", "Librarian", new { area = "Librarian" });
                }
                else if (await _userManager.IsInRoleAsync(currentWorkingUser, "Admin"))
                {
                    return RedirectToAction("Index", "Admin", new { area = "Admin" });
                }
            }

            if (ModelState.IsValid == false)
            {
                return View(messageDTO);
            }

            await _messagesCreatorService.CreateMessage(messageDTO);

            return RedirectToAction("Index", "Home");
        }
    }
}
