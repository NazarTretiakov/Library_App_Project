using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Repositories
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly LibraryDbContext _db;

        public AuthorsRepository(LibraryDbContext db)
        {
            _db = db;
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await _db.Authors.Include(a => a.Books)
                                    .ToListAsync();
        }

        public async Task<Author> GetAuthor(string firstname, string lastname)
        {
            return await _db.Authors.Include(a => a.Books)
                                   .FirstOrDefaultAsync(a => a.Firstname == firstname && a.Lastname == lastname);
        }
    }
}
