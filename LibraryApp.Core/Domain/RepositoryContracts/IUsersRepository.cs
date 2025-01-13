using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.DTO;
using System.Linq.Expressions;

namespace LibraryApp.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing User entity.
    /// </summary>
    public interface IUsersRepository
    {
        /// <summary>
        /// Retrieves all users from the data store.
        /// </summary>
        /// <returns>The list of User objects.</returns>
        Task<List<User>> GetAllUsers();

        /// <summary>
        /// Retrieves user from the data store.
        /// </summary>
        /// <param name="userId">Id of the user that will be retrieved.</param>
        /// <returns>User object or null.</returns>
        Task<User> GetUser(string userId);

        /// <summary>
        /// Changes firstname, lastname and description of user based on the entered values.
        /// </summary>
        /// <param name="user">User object in which data will be changed.</param>
        /// <param name="changeProfileInformationDTO">Data Transfer Object with new data of user profile.</param>
        /// <returns>User object with changed data.</returns>
        public Task<User> ChangeUserInformation(User user, ChangeProfileInformationDTO changeProfileInformationDTO);
    }
}
