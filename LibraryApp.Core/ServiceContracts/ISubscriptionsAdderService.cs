using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for adding new subscriptions.
    /// </summary>
    public interface ISubscriptionsAdderService
    {
        /// <summary>
        /// Adds a subscription to the system.
        /// </summary>
        /// <param name="user">The user object on which subscriber is subscribed.</param>
        /// <param name="subscriber">The subscriber object.</param>
        /// <returns>True if subscription was added. Otherwise false.</returns>
        Task<Subscription> AddSubscription(User user, User subscriber);
    }
}
