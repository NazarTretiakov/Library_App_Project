using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.UI.Areas.Librarian.Controllers
{
    [Area("Librarian")]
    [Authorize(Roles = "Librarian")]
    public class LibrarianController : Controller
    {
        private readonly IAuthorsGetterService _authorsGetterService;
        private readonly IAuthorsAdderService _authorsAdderService;
        private readonly IAuthorsRemoverService _authorsRemoverService;
        private readonly IBooksAdderService _booksAdderService;
        private readonly IBooksGetterService _booksGetterService;
        private readonly IChangeBookAmountService _changeBookAmountService;
        private readonly IBooksRemoverService _booksRemoverService;

        public LibrarianController(IAuthorsGetterService authorsGetterService, IAuthorsAdderService authorsAdderService, IAuthorsRemoverService authorsRemoverService, IBooksAdderService booksAdderService, IBooksGetterService booksGetterService, IChangeBookAmountService changeBookAmountService, IBooksRemoverService booksRemoverService)
        {
            _authorsGetterService = authorsGetterService;
            _authorsAdderService = authorsAdderService;
            _authorsRemoverService = authorsRemoverService;
            _booksAdderService = booksAdderService;
            _booksGetterService = booksGetterService;
            _changeBookAmountService = changeBookAmountService;
            _booksRemoverService = booksRemoverService;
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

            books = books.OrderByDescending(b => b.Title).ToList();

            return View(books);
        }

        [Route("/librarian-panel/manage-books/add-book")]
        [HttpGet]
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

        [Route("/librarian-panel/manage-books/manage-book/change-amount")]
        [HttpPost]
        public async Task<IActionResult> ChangeBookAmount(ChangeBookAmountDTO changeBookAmountDTO)
        {
            if (!Guid.TryParse(changeBookAmountDTO.BookId, out Guid result))
            {
                return NotFound();  //TODO: create custom exception page for that type of situations (input postId is not in the correct format, or postId is not present in the query string)
            }

            Book book = await _booksGetterService.GetBookByBookId(changeBookAmountDTO.BookId);

            book = await _changeBookAmountService.ChangeBookAmount(book, changeBookAmountDTO.NewAmount);

            return RedirectToAction(nameof(LibrarianController.ManageBook), "Librarian", new { bookId = book.BookId });
        }

        [Route("/librarian-panel/manage-books/manage-book/delete-book")]
        [HttpPost]
        public async Task<IActionResult> DeleteBook(string bookId)
        {
            if (!Guid.TryParse(bookId, out Guid result))
            {
                return NotFound();  //TODO: create custom exception page for that type of situations (input postId is not in the correct format, or postId is not present in the query string)
            }

            Book book = await _booksGetterService.GetBookByBookId(bookId);

            await _booksRemoverService.DeleteBook(book);

            return RedirectToAction(nameof(LibrarianController.ManageBooks), "Librarian");
        }

        [Route("/librarian-panel/manage-authors")]
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

        [Route("/librarian-panel/manage-authors/add-author")]
        [HttpGet]
        public IActionResult AddAuthor()
        {
            return View();
        }

        [Route("/librarian-panel/manage-authors/add-author")]
        [HttpPost]
        public async Task<IActionResult> AddAuthor(AuthorDTO authorDTO)
        {
            if (ModelState.IsValid == false)
            {
                return View(authorDTO);
            }

            Author createdAuthor = await _authorsAdderService.AddAuthor(authorDTO);

            return RedirectToAction(nameof(LibrarianController.ManageAuthors), "Librarian", new { searchString = createdAuthor.Firstname + " " + createdAuthor.Lastname });
        }

        [Route("/librarian-panel/manage-authors/manage-author/delete-author")]
        public async Task<IActionResult> DeleteAuthor(string authorId)
        {
            if (!Guid.TryParse(authorId, out Guid result))
            {
                return NotFound();  //TODO: create custom exception page for that type of situations (input postId is not in the correct format, or postId is not present in the query string)
            }

            Author author = await _authorsGetterService.GetAuthorByAuthorId(authorId);

            await _authorsRemoverService.DeleteAuthor(author);

            return RedirectToAction(nameof(LibrarianController.ManageAuthors), "Librarian");
        }
    }
}
