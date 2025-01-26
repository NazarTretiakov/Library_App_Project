using LibraryApp.Core.Domain.Entities;

namespace LibraryApp.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Genre entity
    /// </summary>
    public interface IGenresRepository
    {
        /// <summary>
        /// Returns all genres from the data store
        /// </summary>
        /// <returns>List of Genre objects</returns>
        Task<List<Genre>> GetAllGenres();

        /// <summary>
        /// Returns genre by it's name from the data store
        /// </summary>
        /// <param name="genreName">Name of genre</param>
        /// <returns>Genre object or null</returns>
        Task<Genre> GetGenre(string genreName);

        /// <summary>
        /// Adds genre to the data store
        /// </summary>
        /// <param name="genre">Genre object that will be added to the data store</param>
        /// <returns>True if genre was added. Otherwise false</returns>
        Task<bool> AddGenre(Genre genre);
    }
}
