using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.DTO;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for changing profile information of current working user.
    /// </summary>
    public interface IChangeProfileInformationService
    {
        /// <summary>
        /// Changes profile information of current working user.
        /// </summary>
        /// <param name="changeProfileInformationDTO">Data Transfer Object with data of new profile information.</param>
        /// <returns>User object with changed profile information.</returns>
        Task<User> ChangeProfileInformation(ChangeProfileInformationDTO changeProfileInformationDTO);
    }
}
