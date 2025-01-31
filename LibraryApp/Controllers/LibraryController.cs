using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.UI.Controllers
{
    [Authorize(Roles = "User")]
    public class LibraryController : Controller
    {
        private readonly IBooksGetterService _booksGetterService;
        private readonly IToggleSaveService _toggleSaveService;
        private readonly IIsBookSavedService _isBookSavedService;
        private readonly IReviewsCreatorService _reviewsCreatorService;

        private readonly UserManager<User> _userManager;

        public LibraryController(IBooksGetterService booksGetterService, IToggleSaveService toggleSaveService, IIsBookSavedService isBookSavedService, IReviewsCreatorService reviewsCreatorService, UserManager<User> userManager)
        {
            _booksGetterService = booksGetterService;
            _toggleSaveService = toggleSaveService;
            _isBookSavedService = isBookSavedService;
            _reviewsCreatorService = reviewsCreatorService;
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
                return NotFound();  //TODO: create custom exception page for that type of situations (input searchString is not correct)
            }
            else
            {
                books = await _booksGetterService.GetFilteredBooks(searchFilter, searchString);
            }

            books = books.OrderByDescending(p => p.Title).ToList();

            return View(books);
        }

        [Route("/library/book")]
        public async Task<IActionResult> Book(string bookId)   //  TODO: Fix the bug where the old user profile image is not removed when the user updates their profile image.The old image should be removed.
        {
            if (!Guid.TryParse(bookId, out Guid result))
            {
                return NotFound();  //TODO: create custom exception page for that type of situations (input postId is not in the correct format, or postId is not present in the query string)
            }

            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            Book book = await _booksGetterService.GetBookByBookId(bookId);

            if (book == null)
            {
                return NotFound();  //TODO: create custom exception page for that type of situations (when the post is not found in db)
            }

            ViewBag.IsSaved = await _isBookSavedService.IsBookSaved(book.BookId.ToString());

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

        [Route("/library/book/leave-review")]
        [HttpGet]
        public IActionResult LeaveReview(string bookId)
        {
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
