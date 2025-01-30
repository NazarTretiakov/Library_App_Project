namespace LibraryApp.Core.ServiceContracts
{
    /// <summary>
    /// Represent the service for checking if the book is saved by current working user.
    /// </summary>
    public interface IIsBookSavedService
    {
        /// <summary>
        /// Checks if the book is saved by current woring user.
        /// </summary>
        /// <param name="bookId">The id of the book that will be checked.</param>
        /// <returns>True if the book is saved by the current working user. Otherwise false.</returns>
        Task<bool> IsBookSaved(string bookId);
    }
}
