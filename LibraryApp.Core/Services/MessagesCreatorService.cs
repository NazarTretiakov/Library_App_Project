using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Core.Services
{
    public class MessagesCreatorService : IMessagesCreatorService
    {
        private readonly IMessagesRepository _messagesRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public MessagesCreatorService(IMessagesRepository messagesRepository, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _messagesRepository = messagesRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<Message> CreateMessage(MessageDTO messageDTO)
        {
            Message newMessage = new Message()
            {
                User = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User),
                Content = messageDTO.Content,
                DateOfPublication = DateTime.Now
            };

            await _messagesRepository.CreateMessage(newMessage);

            return newMessage;
        }
    }
}
