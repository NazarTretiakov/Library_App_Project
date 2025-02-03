using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Infrastructure.DbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<List<Author>> GetFilteredAuthors(Expression<Func<Author, bool>> predicate)
        {
            return await _db.Authors.Include(a => a.Books)
                                    .Where(predicate)
                                    .ToListAsync();
        }

        public async Task<bool> AddAuthor(Author author)
        {
            await _db.Authors.AddAsync(author);

            var rowsAdded = await _db.SaveChangesAsync();

            return rowsAdded > 0;
        }

        public async Task<bool> DeleteAuthor(Author author)
        {
            _db.Authors.Remove(author);

            var rowsAffected = await _db.SaveChangesAsync();

            return rowsAffected < 0;
        }

        public async Task<Author> GetAuthor(string authorId)
        {
            return await _db.Authors.Include(a => a.Books)
                                    .FirstOrDefaultAsync(a => a.AuthorId == Guid.Parse(authorId));
        }
    }
}
