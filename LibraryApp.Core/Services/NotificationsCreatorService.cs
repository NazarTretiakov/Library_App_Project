using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.DTO;
using LibraryApp.Core.Enums;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Core.Services
{
    public class NotificationsCreatorService : INotificationsCreatorService
    {
        private readonly INotificationsRepository _notificationsRepository;
        private readonly IUsersGetterService _usersGetterService;
        private readonly IBooksGetterService _booksGetterService;

        private readonly UserManager<User> _userManager;

        public NotificationsCreatorService(INotificationsRepository notificationsRepository, IUsersGetterService usersGetterService, IBooksGetterService booksGetterService, UserManager<User> userManager)
        {
            _notificationsRepository = notificationsRepository;
            _usersGetterService = usersGetterService;
            _booksGetterService = booksGetterService;
            _userManager = userManager;
        }

        public async Task<Notification> CreateNotification(NotificationDTO notificationDTO)
        {
            Notification newNotification;
            User user = null;
            Book book = null;

            if (notificationDTO.ObjectId != null)
            {
                user = await _usersGetterService.GetUserByUserId(notificationDTO.ObjectId);
                book = await _booksGetterService.GetBookByBookId(notificationDTO.ObjectId);
            }

            if (notificationDTO.ObjectId == null)
            {
                newNotification = new Notification()
                {
                    NotificationId = Guid.NewGuid(),
                    Content = notificationDTO.Content,
                    NotificationReceiver = await _usersGetterService.GetUserByUserId(notificationDTO.ReceiverId),
                    NotificationType = NotificationTypeOptions.Library.ToString(),
                    DateOfCreation = DateTime.Now
                };
            }
            else if (user == null)
            {
                newNotification = new Notification()
                {
                    NotificationId = Guid.NewGuid(),
                    Content = notificationDTO.Content,
                    Book = book,
                    NotificationType = NotificationTypeOptions.Book.ToString(),
                    NotificationReceiver = await _usersGetterService.GetUserByUserId(notificationDTO.ReceiverId),
                    DateOfCreation = DateTime.Now
                };
            }
            else
            {
                newNotification = new Notification()
                {
                    NotificationId = Guid.NewGuid(),
                    Content = notificationDTO.Content,
                    User = user,
                    NotificationType = NotificationTypeOptions.User.ToString(),
                    NotificationReceiver = await _usersGetterService.GetUserByUserId(notificationDTO.ReceiverId),
                    DateOfCreation = DateTime.Now
                };
            }

            await _notificationsRepository.CreateNotification(newNotification);

            return newNotification;
        }

        public async Task<bool> CreateNotificationForAdmins(NotificationForAdminsDTO notificationForAdminsDTO)
        {
            List<Notification> newNotifications = new List<Notification>();
            User user = null;
            Book book = null;

            List<User> allUsers = await _usersGetterService.GetAllUsers();
            List<User> receivers = new List<User>();

            foreach (User u in allUsers)
            {
                if (await _userManager.IsInRoleAsync(u, UserRoleOptions.Librarian.ToString()) || await _userManager.IsInRoleAsync(u, UserRoleOptions.Admin.ToString()))
                {
                    receivers.Add(u);
                }
            }

            if (notificationForAdminsDTO.ObjectId != null)
            {
                user = await _usersGetterService.GetUserByUserId(notificationForAdminsDTO.ObjectId);
                book = await _booksGetterService.GetBookByBookId(notificationForAdminsDTO.ObjectId);
            }

            if (notificationForAdminsDTO.ObjectId == null)
            {
                foreach (User receiver in receivers)
                {
                    Notification newNotification = new Notification()
                    {
                        NotificationId = Guid.NewGuid(),
                        Content = notificationForAdminsDTO.Content,
                        NotificationReceiver = receiver,
                        NotificationType = NotificationTypeOptions.Library.ToString(),
                        DateOfCreation = DateTime.Now
                    };

                    newNotifications.Add(newNotification);
                }
            }
            else if (user == null)
            {
                foreach (User receiver in receivers)
                {
                    Notification newNotification = new Notification()
                    {
                        NotificationId = Guid.NewGuid(),
                        Content = notificationForAdminsDTO.Content,
                        Book = book,
                        NotificationType = NotificationTypeOptions.Book.ToString(),
                        NotificationReceiver = receiver,
                        DateOfCreation = DateTime.Now
                    };

                    newNotifications.Add(newNotification);
                }
            }
            else
            {
                foreach (User receiver in receivers)
                {
                    Notification newNotification = new Notification()
                    {
                        NotificationId = Guid.NewGuid(),
                        Content = notificationForAdminsDTO.Content,
                        User = user,
                        NotificationType = NotificationTypeOptions.User.ToString(),
                        NotificationReceiver = receiver,
                        DateOfCreation = DateTime.Now
                    };

                    newNotifications.Add(newNotification);
                }
            }

            foreach (Notification newNotification in newNotifications)
            {
                await _notificationsRepository.CreateNotification(newNotification);
            }

            return true;
        }
    }
}
