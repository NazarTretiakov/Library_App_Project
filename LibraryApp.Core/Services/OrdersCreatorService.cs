using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.DTO;
using LibraryApp.Core.Enums;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Core.Services
{
    public class OrdersCreatorService : IOrdersCreatorService
    {
        private readonly IBooksGetterService _booksGetterService;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IBooksRepository _booksRepository;
        private readonly INotificationsCreatorService _notificationCreatorService;
        private readonly IUsersGetterService _usersGetterService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public OrdersCreatorService(IBooksGetterService booksGetterService, IOrdersRepository ordersRepository, IBooksRepository booksRepository, IHttpContextAccessor httpContextAccessor, INotificationsCreatorService notificationsCreatorService, IUsersGetterService usersGetterService, UserManager<User> userManager)
        {
            _booksGetterService = booksGetterService;
            _ordersRepository = ordersRepository;
            _booksRepository = booksRepository;
            _httpContextAccessor = httpContextAccessor;
            _notificationCreatorService = notificationsCreatorService;
            _usersGetterService = usersGetterService;
            _userManager = userManager;
        }

        public async Task<Order> CreateOrder(string bookId)
        {
            Book orderedBook = await _booksGetterService.GetBookByBookId(bookId);
            User user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            await _booksRepository.ChangeBookHoldsAmount(orderedBook, orderedBook.Holds + 1);

            Order newOrder = new Order()
            {
                User = user,
                Book = orderedBook,
                Status = OrderStatusOptions.Ordered.ToString(),
                DateOfOrder = DateTime.Now,
                IsChecked = false
            };

            await _ordersRepository.AddOrder(newOrder);

            await _notificationCreatorService.CreateNotificationForAdmins(new NotificationForAdminsDTO()
            {
                Content = $"{user.UserName} has ordered the book \"{orderedBook.Title}\".",
                ObjectId = user.Id.ToString()
            });

            return newOrder;
        }
    }
}
