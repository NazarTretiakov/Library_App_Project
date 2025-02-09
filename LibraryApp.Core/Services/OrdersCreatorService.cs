using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.Enums;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Core.Services
{
    public class OrdersCreatorService : IOrdersCreatorService
    {
        private readonly IBooksGetterService _booksGetterService;
        public readonly IOrdersRepository _ordersRepository;
        public readonly IBooksRepository _booksRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public OrdersCreatorService(IBooksGetterService booksGetterService, IOrdersRepository ordersRepository, IBooksRepository booksRepository, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _booksGetterService = booksGetterService;
            _ordersRepository = ordersRepository;
            _booksRepository = booksRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<Order> CreateOrder(string bookId)
        {
            Book orderedBook = await _booksGetterService.GetBookByBookId(bookId);

            await _booksRepository.ChangeBookHoldsAmount(orderedBook, orderedBook.Holds + 1);

            Order newOrder = new Order()
            {
                User = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User),
                Book = orderedBook,
                Status = OrderStatusOptions.Ordered.ToString(),
                DateOfOrder = DateTime.Now,
                IsChecked = false
            };

            await _ordersRepository.AddOrder(newOrder);

            return newOrder;
        }
    }
}
