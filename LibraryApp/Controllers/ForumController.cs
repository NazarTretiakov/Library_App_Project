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

        [Route("/forum/post")]
        public IActionResult Post()
        {
            return View();
        }

        [Route("/forum/create-post")]
        public IActionResult CreatePost()
        {
            return View();
        }
    }
}
