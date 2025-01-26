using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Repositories
{
    public class GenresRepository : IGenresRepository
    {
        private readonly LibraryDbContext _db;

        public GenresRepository(LibraryDbContext db)
        {
            _db = db;
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            return await _db.Genres.Include(g => g.Books).ToListAsync();
        }

        public async Task<Genre> GetGenre(string genreName)
        {
            return await _db.Genres.Include(g => g.Books).FirstOrDefaultAsync(genre => genre.Name == genreName);
        }

        public async Task<bool> AddGenre(Genre genre)
        {
            await _db.Genres.AddAsync(genre);

            var entitiesAddedToDb = await _db.SaveChangesAsync();

            return entitiesAddedToDb > 0;
        }
    }
}
