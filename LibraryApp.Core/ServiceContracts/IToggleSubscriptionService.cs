namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for toggling subscription.
    /// </summary>
    public interface IToggleSubscriptionService
    {
        /// <summary>
        /// Toggles subscription on the user for current user in the system.
        /// </summary>
        /// <param name="userId">The id of the user on which subscription will be toggled.</param>
        /// <returns>True if subscription is active, false if subsctiption is inactive.</returns>
        Task<bool> ToggleSubscription(string userId);
    }
}
