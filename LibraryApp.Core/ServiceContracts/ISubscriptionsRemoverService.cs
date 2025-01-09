using LibraryApp.Core.Domain.Entities;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for removing subscriptions.
    /// </summary>
    public interface ISubscriptionsRemoverService
    {
        /// <summary>
        /// Removes subscription from the system.
        /// </summary>
        /// <param name="subscription">Subscription object that will be removed.</param>
        /// <returns>True if subscription was removed. Otherwise false.</returns>
        Task<bool> RemoveSubscription(Subscription subscription);
    }
}
