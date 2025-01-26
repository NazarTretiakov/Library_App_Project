using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryApp.Infrastructure.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly LibraryDbContext _db;

        public BooksRepository(LibraryDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddBook(Book book)
        {
            await _db.Books.AddAsync(book);

            var rowsAdded = await _db.SaveChangesAsync();

            return rowsAdded > 0;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _db.Books.Include(b => b.Author)
                                  .Include(b => b.Genres)
                                  .ToListAsync();
        }

        public async Task<Book> GetBook(string bookId)
        {
            return await _db.Books.Include(b => b.Author)
                                  .Include(b => b.Genres)
                                  .FirstOrDefaultAsync(b => b.BookId == Guid.Parse(bookId));
        }

        public async Task<List<Book>> GetFilteredBooks(Expression<Func<Book, bool>> predicate)
        {
            return await _db.Books.Include(b => b.Author)
                                  .Include(b => b.Genres)
                                  .Where(predicate)
                                  .ToListAsync();
        }
    }
}
