using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.UI.Areas.Librarian.Controllers
{
    [Area("Librarian")]
    [Authorize(Roles = "Librarian")]
    public class LibrarianController : Controller
    {
        [Route("/librarian-panel")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
