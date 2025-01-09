using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;

namespace LibraryApp.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Subscriber entity.
    /// </summary>
    public interface ISubscriptionsRepository
    {
        /// <summary>
        /// Retrieves subscription from the data store.
        /// </summary>
        /// <param name="subscriptionId">Id of the subscription that will be retrieved.</param>
        /// <returns>Subscription object or null.</returns>
        Task<Subscription> GetSubscription(string subscriptionId);

        /// <summary>
        /// Retrieves subscription from the data store.
        /// </summary>
        /// <param name="userId">The id of the user on which subscriber is subscribed.</param>
        /// <param name="subscriberId">The id of the subscriber.</param>
        /// <returns>Subscription object or null.</returns>
        Task<Subscription> GetSubscription(string userId, string subscriberId);

        /// <summary>
        /// Retrieves all subscribers of the user from the data store.
        /// </summary>
        /// <param name="userId">The id of user which subscribers will be retrieved.</param>
        /// <returns>List of Subscription objects or null.</returns>
        Task<List<Subscription>> GetUserSubscribers(string userId);

        /// <summary>
        /// Retrieves all subscriptions of the user from the data store.
        /// </summary>
        /// <param name="userId">The id of user which subscriptions will be retrieved.</param>
        /// <returns>List of Subscription objects or null.</returns>
        Task<List<Subscription>> GetUserSubscriptions(string userId);

        /// <summary>
        /// Adds subscription to the data store.
        /// </summary>
        /// <param name="user">The user object on which subscriber is subscribed.</param>
        /// <param name="subscriber">The subscriber object.</param>
        /// <returns>True if subscription was added. Otherwise false.</returns>
        Task<bool> AddSubscription(User user, User subscriber);

        /// <summary>
        /// Removes subscription from the data store.
        /// </summary>
        /// <param name="subscription">Subscription object which will be removed.</param>
        /// <returns>True if subscription was removed. Otherwise false.</returns>
        Task<bool> RemoveSubscription(Subscription subscription);
    }
}
