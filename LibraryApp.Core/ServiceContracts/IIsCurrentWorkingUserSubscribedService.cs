namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represent the service for checking if the current working user is subscribed on user.
    /// </summary>
    public interface IIsCurrentWorkingUserSubscribedService
    {
        /// <summary>
        /// Checks if the current working user is subscribed on user.
        /// </summary>
        /// <param name="userId">The id of the user on which subscription will be checked.</param>
        /// <returns>True if the current working user is subscribed. Otherwise false.</returns>
        Task<bool> IsCurrentWorkingUserSubscribed(string userId);
    }
}
