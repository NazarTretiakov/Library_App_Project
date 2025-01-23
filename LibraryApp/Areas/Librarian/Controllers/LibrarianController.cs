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

        [Route("/librarian-panel/manage-books")]
        public IActionResult ManageBooks()
        {
            return View();
        }

        [Route("/librarian-panel/manage-books/add-book")]
        public IActionResult AddBook()
        {
            return View();
        }

        [Route("/librarian-panel/manage-books/manage-book")]
        public IActionResult ManageBook()
        {
            return View();
        }
    }
}
