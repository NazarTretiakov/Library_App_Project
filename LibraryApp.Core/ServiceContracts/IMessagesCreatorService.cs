using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.DTO;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for adding new message.
    /// </summary>
    public interface IMessagesCreatorService
    {
        /// <summary>
        /// Adds a message to the system.
        /// </summary>
        /// <param name="messageDTO">Data Transfer Object which contains data for creating the message.</param>
        /// <returns>True if message was added. Otherwise false.</returns>
        Task<Message> CreateMessage(MessageDTO messageDTO);
    }
}
