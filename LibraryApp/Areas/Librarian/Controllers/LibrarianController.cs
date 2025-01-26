using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ServiceContracts;
using LibraryApp.Core.Services;
using LibraryApp.UI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibraryApp.UI.Areas.Librarian.Controllers
{
    [Area("Librarian")]
    [Authorize(Roles = "Librarian")]
    public class LibrarianController : Controller
    {
        private readonly IAuthorsGetterService _authorsGetterService;
        private readonly IBooksAdderService _booksAdderService;
        private readonly IBooksGetterService _booksGetterService;

        public LibrarianController(IAuthorsGetterService authorsGetterService, IBooksAdderService booksAdderService, IBooksGetterService booksGetterService)
        {
            _authorsGetterService = authorsGetterService;
            _booksAdderService = booksAdderService;
            _booksGetterService = booksGetterService;
        }

        [Route("/librarian-panel")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/librarian-panel/manage-books")]
        public async Task<IActionResult> ManageBooks(string searchString, string searchFilter = "all")
        {
            List<Book> books;

            ViewBag.SearchString = searchString;
            ViewBag.SearchFilter = searchFilter;

            if (searchString == null)
            {
                books = await _booksGetterService.GetAllBooks();
            }
            else if (searchFilter != "all" && searchFilter != "authorName" && searchFilter != "title" && searchFilter != "genre")
            {
                return NotFound();  //TODO: create custom exception page for that type of situations (input searchString is not correct)
            }
            else
            {
                books = await _booksGetterService.GetFilteredBooks(searchFilter, searchString);
            }

            books = books.OrderByDescending(p => p.Title).ToList();

            return View(books);
        }

        [Route("/librarian-panel/manage-books/add-book")]
        public IActionResult AddBook()
        {
            return View();
        }

        [Route("/librarian-panel/manage-books/add-book")]
        [HttpPost]
        public async Task<IActionResult> AddBook(BookDTO bookDTO)
        {
            Author author = await _authorsGetterService.GetAuthorByFullName(bookDTO.AuthorFirstname, bookDTO.AuthorLastname);

            if (author == null)
            {
                ModelState.AddModelError("Adding book", "Author with specified firstname and lastname hasn't present in the system. (Add author to the system or check the spelling of entered data)");
            }

            if (ModelState.IsValid == false)
            {
                return View(bookDTO);
            }

            Book createdBook = await _booksAdderService.AddBook(bookDTO);

            return RedirectToAction(nameof(LibrarianController.ManageBook), "Librarian", new { bookId = createdBook.BookId });
        }

        [Route("/librarian-panel/manage-books/manage-book")]
        [HttpGet]
        public async Task<IActionResult> ManageBook(string? bookId)
        {
            if (!Guid.TryParse(bookId, out Guid result))
            {
                return NotFound();  //TODO: create custom exception page for that type of situations (input postId is not in the correct format, or postId is not present in the query string)
            }

            Book book = await _booksGetterService.GetBookByBookId(bookId);

            return View(book);
        }
    }
}
