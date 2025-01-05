namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represent the service for checking if the post is liked by current working user.
    /// </summary>
    public interface IIsPostLikedService
    {
        /// <summary>
        /// Checks if the post is liked by current woring user.
        /// </summary>
        /// <param name="postId">The id of the post that will be checked.</param>
        /// <returns>True if the post is liked by the current working user. Otherwise false.</returns>
        Task<bool> IsPostLiked(string postId);
    }
}
