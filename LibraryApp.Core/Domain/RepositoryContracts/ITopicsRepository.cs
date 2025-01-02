using LibraryApp.Core.Domain.Entities;

namespace LibraryApp.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Topic entity
    /// </summary>
    public interface ITopicsRepository
    {
        /// <summary>
        /// Returns all topics from the data store
        /// </summary>
        /// <returns>List of Topic objects</returns>
        Task<List<Topic>> GetAllTopics();

        /// <summary>
        /// Returns topic by it's name from the data store
        /// </summary>
        /// <param name="topicName">Name of topic</param>
        /// <returns>Topic object or null</returns>
        Task<Topic> GetTopic(string topicName);

        /// <summary>
        /// Adds topic to the data store
        /// </summary>
        /// <param name="topic">Topic object that will be added to the data store</param>
        /// <returns>True if topic was added. Otherwise false</returns>
        Task<bool> AddTopic(Topic topic);
    }
}
