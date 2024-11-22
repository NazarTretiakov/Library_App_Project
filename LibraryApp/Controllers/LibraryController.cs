using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.UI.Controllers
{
    public class LibraryController : Controller
    {
        [Route("/library")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
