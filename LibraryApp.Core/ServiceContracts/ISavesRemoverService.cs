using LibraryApp.Core.Domain.Entities;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for removing saves.
    /// </summary>
    public interface ISavesRemoverService
    {
        /// <summary>
        /// Removes save from the system.
        /// </summary>
        /// <param name="save">Save object that will be removed.</param>
        /// <returns>True if save was removed. Otherwise false.</returns>
        Task<bool> RemoveSave(Save save);
    }
}
