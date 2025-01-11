using LibraryApp.Core.Domain.Entities;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for retrieving comments from db.
    /// </summary>
    public interface ICommentsGetterService
    {
        /// <summary>
        /// Retrieves all comments that were made by particular user from the system.
        /// </summary>
        /// <param name="userId">The id of the user which commentaries will be retrieved.</param>
        /// <returns>List of Comment objects or null</returns>
        Task<List<Comment>> GetUserComments(string userId);

        /// <summary>
        /// Retrieves filtered comments that were made by particular user form the system.
        /// </summary>
        /// <param name="userId">The id of the user which commentaries will be retrieved.</param>
        /// <param name="searchFilter">The name of the filter that will be used to search.</param>
        /// <param name="searchString">The phrase to be searched for.</param>
        /// <returns>List of Comment objects or null</returns>
        Task<List<Comment>> GetUserFilteredComments(string userId, string searchFilter, string searchString);
    }
}
