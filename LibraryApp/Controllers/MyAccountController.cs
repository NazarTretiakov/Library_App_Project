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
    }
}
