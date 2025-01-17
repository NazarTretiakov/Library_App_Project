using LibraryApp.Core.DTO;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for changing profile photo of the current user.
    /// </summary>
    public interface IChangeProfilePhotoService
    {
        /// <summary>
        /// Changes profile photo of the current user.
        /// </summary>
        /// <param name="changeProfilePhotoDTO">Data Transfer Object with data of new profile photo.</param>
        /// <returns>True if the photo was changed, otherwise false.</returns>
        Task<bool> ChangeProfilePhoto(ChangeProfilePhotoDTO changeProfilePhotoDTO);
    }
}
