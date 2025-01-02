using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.UI.Controllers
{
    public class MyAccountController : Controller
    {
        [Route("/my-account")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/my-account/settings")]
        public IActionResult Settings()
        {
            return RedirectToAction(nameof(MyAccountController.ProfileInformationSettings), "MyAccount");
        }

        [Route("/my-account/settings/profile")]
        public IActionResult ProfileInformationSettings()
        {
            return View();
        }
    }
}
