using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class MessagesGetterService : IMessagesGetterService
    {
        private readonly IMessagesRepository _messagesRepository;

        public MessagesGetterService(IMessagesRepository messagesRepository)
        {
            _messagesRepository = messagesRepository;
        }

        public Task<List<Message>> GetAllMessages()
        {
            return _messagesRepository.GetAllMessages();
        }
    }
}
