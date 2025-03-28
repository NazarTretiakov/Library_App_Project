﻿using LibraryApp.Core.Domain.Entities;

namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represents the service for retrieving subscriptions from db.
    /// </summary>
    public interface ISubscriptionsGetterService
    {
        /// <summary>
        /// Retrieves subscription from the system.
        /// </summary>
        /// <param name="userId">The id of the user on which subscriber is subscribed.</param>
        /// <param name="subscriberId">The id of the subscriber.</param>
        /// <returns>Subscription object or null.</returns>
        Task<Subscription> GetSubscriptionByUserIdAndSubscriberId(string userId, string subscriberId);

        /// <summary>
        /// Retrieves all subscribers of the user from the system.
        /// </summary>
        /// <param name="userId">The id of user which subscribers will be retrieved.</param>
        /// <returns>List of Subscription objects or null.</returns>
        Task<List<Subscription>> GetUserSubscribers(string userId);

        /// <summary>
        /// Retrieves all subscriptions of the user from the system.
        /// </summary>
        /// <param name="userId">The id of user which subscriptions will be retrieved.</param>
        /// <returns>List of Subscription objects or null.</returns>
        Task<List<Subscription>> GetUserSubscriptions(string userId);

        /// <summary>
        /// Retrieves filtered subscribers of the user from the system.
        /// </summary>
        /// <param name="userId">The id of user which subscribers will be retrieved.</param>
        /// <param name="searchString">The phrase to be searched for.</param>
        /// <param name="searchFilter">The name of the filter that will be used to search.</param>
        /// <returns>List of Subscription objects or null.</returns>
        Task<List<Subscription>> GetUserFilteredSubscribers(string userId, string searchFilter, string searchString);

        /// <summary>
        /// Retrieves filtered subscriptions of the user from the system.
        /// </summary>
        /// <param name="userId">The id of user which subscriptions will be retrieved.</param>
        /// <param name="searchString">The phrase to be searched for.</param>
        /// <param name="searchFilter">The name of the filter that will be used to search.</param>
        /// <returns>List of Subscription objects or null.</returns>
        Task<List<Subscription>> GetUserFilteredSubscriptions(string userId, string searchFilter, string searchString);

    }
}
