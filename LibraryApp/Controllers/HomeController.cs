using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.UI.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        [Route("/home")]
        [AllowAnonymous]
        public IActionResult Index()
        {
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
