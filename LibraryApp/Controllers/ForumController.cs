using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.UI.Controllers
{
    public class ForumController : Controller
    {
        [Route("/forum")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
