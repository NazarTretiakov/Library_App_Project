using LibraryApp.Core.Domain.Entities;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for retrieving saves from db.
    /// </summary>
    public interface ISavesGetterService
    {
        /// <summary>
        /// Retrieves save from the system.
        /// </summary>
        /// <param name="userId">The id of user which saved the post.</param>
        /// <param name="objectId">The id of post or book which was saved.</param>
        /// <returns>Save object or null.</returns>
        Task<Save> GetSaveByUserIdAndObjectId(string userId, string objectId);

        /// <summary>
        /// Retrieves saves of post or book from the system.
        /// </summary>
        /// <param name="objectId">The id of post or book which saves will be retrieved.</param>
        /// <returns>List of Save objects or null.</returns>
        Task<List<Save>> GetSaves(string objectId);
    }
}
