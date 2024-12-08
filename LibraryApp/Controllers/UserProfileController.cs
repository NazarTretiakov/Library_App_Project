using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.UI.Controllers
{
    public class UserProfileController : Controller
    {
        [Route("/user-profile")]
        public IActionResult Index()
        {
            return RedirectToAction("Posts");
        }

        [Route("/user-profile/posts")]
        public IActionResult Posts()
        {
            return View();
        }
    }
}
