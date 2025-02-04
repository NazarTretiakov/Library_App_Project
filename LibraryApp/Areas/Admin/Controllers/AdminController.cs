using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ServiceContracts;
using LibraryApp.Core.Services;
using LibraryApp.UI.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAuthorsGetterService _authorsGetterService;
        private readonly IAuthorsAdderService _authorsAdderService;
        private readonly IAuthorsRemoverService _authorsRemoverService;
        private readonly IBooksAdderService _booksAdderService;
        private readonly IBooksGetterService _booksGetterService;
        private readonly IChangeBookAmountService _changeBookAmountService;
        private readonly IBooksRemoverService _booksRemoverService;

        public AdminController(IAuthorsGetterService authorsGetterService, IAuthorsAdderService authorsAdderService, IAuthorsRemoverService authorsRemoverService, IBooksAdderService booksAdderService, IBooksGetterService booksGetterService, IChangeBookAmountService changeBookAmountService, IBooksRemoverService booksRemoverService)
        {
            _authorsGetterService = authorsGetterService;
            _authorsAdderService = authorsAdderService;
            _authorsRemoverService = authorsRemoverService;
            _booksAdderService = booksAdderService;
            _booksGetterService = booksGetterService;
            _changeBookAmountService = changeBookAmountService;
            _booksRemoverService = booksRemoverService;
        }

        [Route("/admin-panel")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/admin-panel/manage-users")]
        public async Task<IActionResult> ManageUsers()
        {
            return View();
        }

        [Route("/admin-panel/manage-users/manage-user")]
        public async Task<IActionResult> ManageUser()
        {
            return View();
        }

        [Route("/admin-panel/manage-users/manage-user/orders")]
        public async Task<IActionResult> ManageUserOrders()
        {
            return View();
        }

        [Route("/admin-panel/manage-users/manage-user/books")]
        public async Task<IActionResult> ManageUserBooks()
        {
            return View();
        }

        [Route("/admin-panel/manage-books")]
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

        [Route("/admin-panel/manage-books/add-book")]
        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }

        [Route("/admin-panel/manage-books/add-book")]
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

            return RedirectToAction(nameof(AdminController.ManageBook), "Admin", new { bookId = createdBook.BookId });
        }

        [Route("/admin-panel/manage-books/manage-book")]
        public async Task<IActionResult> ManageBook(string? bookId)
        {
            if (!Guid.TryParse(bookId, out Guid result))
            {
                return NotFound();  //TODO: create custom exception page for that type of situations (input postId is not in the correct format, or postId is not present in the query string)
            }

            Book book = await _booksGetterService.GetBookByBookId(bookId);

            return View(book);
        }

        [Route("/admin-panel/manage-books/manage-book/change-amount")]
        [HttpPost]
        public async Task<IActionResult> ChangeBookAmount(ChangeBookAmountDTO changeBookAmountDTO)
        {
            if (!Guid.TryParse(changeBookAmountDTO.BookId, out Guid result))
            {
                return NotFound();  //TODO: create custom exception page for that type of situations (input postId is not in the correct format, or postId is not present in the query string)
            }

            Book book = await _booksGetterService.GetBookByBookId(changeBookAmountDTO.BookId);

            book = await _changeBookAmountService.ChangeBookAmount(book, changeBookAmountDTO.NewAmount);

            return RedirectToAction(nameof(AdminController.ManageBook), "Admin", new { bookId = book.BookId });
        }

        [Route("/admin-panel/manage-books/manage-book/delete-book")]
        [HttpPost]
        public async Task<IActionResult> DeleteBook(string bookId)
        {
            if (!Guid.TryParse(bookId, out Guid result))
            {
                return NotFound();  //TODO: create custom exception page for that type of situations (input postId is not in the correct format, or postId is not present in the query string)
            }

            Book book = await _booksGetterService.GetBookByBookId(bookId);

            await _booksRemoverService.DeleteBook(book);

            return RedirectToAction(nameof(AdminController.ManageBooks), "Admin");
        }

        [Route("/admin-panel/manage-authors")]
        public async Task<IActionResult> ManageAuthors(string searchString, string searchFilter = "all")
        {
            List<Author> authors;

            ViewBag.SearchString = searchString;
            ViewBag.SearchFilter = searchFilter;

            if (searchString == null)
            {
                authors = await _authorsGetterService.GetAllAuthors();
            }
            else if (searchFilter != "all" && searchFilter != "firstname" && searchFilter != "lastname")
            {
                return NotFound();  //TODO: create custom exception page for that type of situations (input searchString is not correct)
            }
            else
            {
                authors = await _authorsGetterService.GetFilteredAuthors(searchFilter, searchString);
            }

            authors = authors.OrderByDescending(a => a.Firstname).ToList();

            return View(authors);
        }

        [Route("/admin-panel/manage-authors/add-author")]
        [HttpGet]
        public IActionResult AddAuthor()
        {
            return View();
        }

        [Route("/admin-panel/manage-authors/add-author")]
        [HttpPost]
        public async Task<IActionResult> AddAuthor(AuthorDTO authorDTO)
        {
            if (ModelState.IsValid == false)
            {
                return View(authorDTO);
            }

            Author createdAuthor = await _authorsAdderService.AddAuthor(authorDTO);

            return RedirectToAction(nameof(AdminController.ManageAuthors), "Admin", new { searchString = createdAuthor.Firstname + " " + createdAuthor.Lastname });
        }

        [Route("/admin-panel/manage-authors/manage-author/delete-author")]
        public async Task<IActionResult> DeleteAuthor(string authorId)
        {
            if (!Guid.TryParse(authorId, out Guid result))
            {
                return NotFound();  //TODO: create custom exception page for that type of situations (input postId is not in the correct format, or postId is not present in the query string)
            }

            Author author = await _authorsGetterService.GetAuthorByAuthorId(authorId);

            await _authorsRemoverService.DeleteAuthor(author);

            return RedirectToAction(nameof(AdminController.ManageAuthors), "Admin");
        }
    }
}
