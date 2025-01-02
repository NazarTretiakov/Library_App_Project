using LibraryApp.Core.Domain.IdentityEntities;

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
    }
}
