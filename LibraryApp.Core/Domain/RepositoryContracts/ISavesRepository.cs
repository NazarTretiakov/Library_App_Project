using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;

namespace LibraryApp.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Save entity.
    /// </summary>
    public interface ISavesRepository
    {
        /// <summary>
        /// Retrieves save from the data store.
        /// </summary>
        /// <param name="saveId">Id of the save that will be retrieved.</param>
        /// <returns>Save object or null.</returns>
        Task<Save> GetSave(string saveId);

        /// <summary>
        /// Retrieves save from the data store.
        /// </summary>
        /// <param name="userId">The id of user which saved the post.</param>
        /// <param name="objectId">The id of post or book which was saved.</param>
        /// <returns>Save object or null.</returns>
        Task<Save> GetSave(string userId, string objectId);

        /// <summary>
        /// Retrieves saves of post or book from the data store.
        /// </summary>
        /// <param name="objectId">The id of post or book which saves will be retrieved.</param>
        /// <returns>List of Save objects or null.</returns>
        Task<List<Save>> GetSavesByObjectId(string objectId);

        /// <summary>
        /// Retrieves all saves made by user from the data store.
        /// </summary>
        /// <param name="userId">The id of the user which saves will be retrieved.</param>
        /// <returns>List of Save objects or null.</returns>
        Task<List<Save>> GetSavesByUserId(string userId);

        /// <summary>
        /// Adds save of post to the data store.
        /// </summary>
        /// <param name="post">Post which was saved.</param>
        /// <param name="user">User that saved a post.</param>
        /// <returns>True if save was added. Otherwise false.</returns>
        Task<bool> AddPostSave(Post post, User user);

        /// <summary>
        /// Adds save of book to the data store.
        /// </summary>
        /// <param name="book">Book which was saved.</param>
        /// <param name="user">User that saved a book.</param>
        /// <returns>True if save was added. Otherwise false.</returns>
        Task<bool> AddBookSave(Book book, User user);

        /// <summary>
        /// Removes save from the data store.
        /// </summary>
        /// <param name="save">Save object which will be removed.</param>
        /// <returns>True if save was removed. Otherwise false.</returns>
        Task<bool> RemoveSave(Save save);
    }
}
