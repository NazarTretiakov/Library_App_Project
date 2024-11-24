using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.UI.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        [Route("/home")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/home/register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("/home/sign-in")]
        public IActionResult SignIn()
        {
            return View();
        }

        [Route("/home/about-us")]
        public IActionResult AboutUs()
        {
            return View();
        }

        [Route("/home/ask-us")]
        public IActionResult AskUs()
        {
            return View();
        }
    }
}
