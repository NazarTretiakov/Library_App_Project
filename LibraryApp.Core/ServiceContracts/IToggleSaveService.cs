using LibraryApp.Core.Domain.Entities;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for toggling save.
    /// </summary>
    public interface IToggleSaveService
    {
        /// <summary>
        /// Toggles save of the post or book for current user in the system.
        /// </summary>
        /// <param name="id">The id of the post or book which save will be toggled.</param>
        /// <returns>True if the save is active, false if like is inactive.</returns>
        Task<bool> ToggleSave(string id);
    }
}
