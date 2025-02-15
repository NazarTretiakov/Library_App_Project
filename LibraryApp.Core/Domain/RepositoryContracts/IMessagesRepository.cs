using LibraryApp.Core.Domain.Entities;

namespace LibraryApp.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Message entity
    /// </summary>
    public interface IMessagesRepository
    {
        /// <summary>
        /// Returns all messages from the data store
        /// </summary>
        /// <returns>List of Message objects</returns>
        Task<List<Message>> GetAllMessages();

        /// <summary>
        /// Adds message to the data store
        /// </summary>
        /// <param name="message">Message object that will be added to the data store</param>
        /// <returns>True if message was added. Otherwise false</returns>
        Task<bool> CreateMessage(Message message);
    }
}
