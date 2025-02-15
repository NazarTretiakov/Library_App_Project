using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.DTO;
using LibraryApp.Core.Enums;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibraryApp.UI.Controllers
{
    [Authorize(Roles = "User")]
    public class LibraryController : Controller
    {
        private readonly IBooksGetterService _booksGetterService;
        private readonly IToggleSaveService _toggleSaveService;
        private readonly IIsBookSavedService _isBookSavedService;
        private readonly IReviewsCreatorService _reviewsCreatorService;
        private readonly IOrdersCreatorService _ordersCreatorService;
        private readonly IOrdersGetterService _ordersGetterService;

        private readonly UserManager<User> _userManager;

        public LibraryController(IBooksGetterService booksGetterService, IToggleSaveService toggleSaveService, IIsBookSavedService isBookSavedService, IReviewsCreatorService reviewsCreatorService, IOrdersCreatorService ordersCreatorService, IOrdersGetterService ordersGetterService, UserManager<User> userManager)
        {
            _booksGetterService = booksGetterService;
            _toggleSaveService = toggleSaveService;
            _isBookSavedService = isBookSavedService;
            _reviewsCreatorService = reviewsCreatorService;
            _ordersCreatorService = ordersCreatorService;
            _ordersGetterService = ordersGetterService;
            _userManager = userManager;
        }

        [Route("/library")]
        public async Task<IActionResult> Index(string searchString, string searchFilter = "all")
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            List<Book> books;

            ViewBag.SearchString = searchString;
            ViewBag.SearchFilter = searchFilter;

            if (searchString == null)
            {
                books = await _booksGetterService.GetAllBooks();
            }
            else if (searchFilter != "all" && searchFilter != "authorName" && searchFilter != "title" && searchFilter != "genre")
            {
                return NotFound();
            }
            else
            {
                books = await _booksGetterService.GetFilteredBooks(searchFilter, searchString);
            }

            books = books.OrderByDescending(p => p.Title).ToList();

            return View(books);
        }

        [Route("/library/book")]
        public async Task<IActionResult> Book(string bookId, bool? error)
        {
            if (!Guid.TryParse(bookId, out Guid result))
            {
                return NotFound();
            }

            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            Book book = await _booksGetterService.GetBookByBookId(bookId);

            if (book == null)
            {
                return NotFound();
            }

            ViewBag.IsSaved = await _isBookSavedService.IsBookSaved(book.BookId.ToString());

            if (error != null)
            {
                ViewBag.Error = true;
            }

            return View(book);
        }

        [Route("/library/book/toggle-book-save")]
        [HttpPost]
        public async Task<IActionResult> ToggleBookSave([FromBody] ToggleBookSaveDTO toggleSaveDTO)
        {
            bool isBookSaved = await _toggleSaveService.ToggleSave(toggleSaveDTO.BookId);
            Book book = await _booksGetterService.GetBookByBookId(toggleSaveDTO.BookId);

            ViewBag.IsSaved = isBookSaved;

            return PartialView("_BookSave", book);
        }

        [Route("/library/book/create-order")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(string bookId)
        {
            if (!Guid.TryParse(bookId, out Guid result))
            {
                return NotFound(); 
            }

            Book book = await _booksGetterService.GetBookByBookId(bookId);

            if (book == null)
            {
                return NotFound();
            }

            if (book.Amount - book.Holds <= 0)
            {
                return RedirectToAction(nameof(LibraryController.Book), "Library", new { bookId = bookId, error = true});
            }

            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            List<Order> userOrders = await _ordersGetterService.GetUserOrders(currentWorkingUser.Id.ToString());
            if (userOrders.FirstOrDefault(o => o.User == currentWorkingUser && o.Book == book && o.Status != OrderStatusOptions.Returned.ToString()) != null)
            {
                return RedirectToAction(nameof(LibraryController.Book), "Library", new { bookId = bookId, error = true });
            }

            await _ordersCreatorService.CreateOrder(bookId);

            return RedirectToAction(nameof(MyAccountController.Orders), "MyAccount");
        }

        [Route("/library/book/leave-review")]
        [HttpGet]
        public IActionResult LeaveReview(string bookId)
        {
            if (!Guid.TryParse(bookId, out Guid result))
            {
                return NotFound();
            }

            return View();
        }

        [Route("/library/book/leave-review")]
        [HttpPost]
        public async Task<IActionResult> LeaveReview(ReviewDTO reviewDTO)
        {
            if (ModelState.IsValid == false)
            {
                return View(reviewDTO);
            }

            await _reviewsCreatorService.CreateReview(reviewDTO);

            return RedirectToAction(nameof(LibraryController.Book), "Library", new { bookId = reviewDTO.BookId });
        }
    }
}
