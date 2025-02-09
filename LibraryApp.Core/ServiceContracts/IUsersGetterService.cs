using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using System.Linq.Expressions;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for retrieving users from db.
    /// </summary>
    public interface IUsersGetterService
    {
        /// <summary>
        /// Retrieves all users from the system.
        /// </summary>
        /// <returns>List of User objects.</returns>
        Task<List<User>> GetAllUsers();

        /// <summary>
        /// Retrieves user from the system.
        /// </summary>
        /// <param name="userId">The id of user that will be retrieved.</param>
        /// <returns>User object or null.</returns>
        Task<User> GetUserByUserId(string userId);

        /// <summary>
        /// Retrieves user from the system.
        /// </summary>
        /// <param name="username">The username of user that will be retrieved.</param>
        /// <returns>User object or null.</returns>
        Task<User> GetUserByUsername(string username);

        /// <summary>
        /// Retrieves filtered users from the system.
        /// </summary>
        /// <param name="searchString">The phrase to be searched for.</param>
        /// <param name="searchFilter">The name of the filter that will be used to search.</param>
        /// <returns>The list of User objects or null.</returns>
        Task<List<User>> GetFilteredUsers(string searchFilter, string searchString);
    }
}
