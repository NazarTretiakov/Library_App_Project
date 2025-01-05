namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represent the service for checking if the post is saved by current working user.
    /// </summary>
    public interface IIsPostSavedService
    {
        /// <summary>
        /// Checks if the post is saved by current woring user.
        /// </summary>
        /// <param name="postId">The id of the post that will be checked.</param>
        /// <returns>True if the post is saved by the current working user. Otherwise false.</returns>
        Task<bool> IsPostSaved(string postId);
    }
}
