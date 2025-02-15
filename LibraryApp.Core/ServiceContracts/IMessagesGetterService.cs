using LibraryApp.Core.Domain.Entities;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for retrieving messages from db.
    /// </summary>
    public interface IMessagesGetterService
    {
        /// <summary>
        /// Retrieves all messages from the system.
        /// </summary>
        /// <returns>List of Message objects.</returns>
        Task<List<Message>> GetAllMessages();
    }
}
