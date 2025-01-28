using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Infrastructure.DbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryApp.Infrastructure.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly LibraryDbContext _db;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksRepository(LibraryDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> AddBook(Book book)
        {
            await _db.Books.AddAsync(book);

            var rowsAdded = await _db.SaveChangesAsync();

            return rowsAdded > 0;
        }

        public async Task<Book> ChangeBookAmount(Book book, int newAmount)
        {
            book.Amount = newAmount;

            await _db.SaveChangesAsync();

            return book;
        }

        public async Task<bool> DeleteBook(Book book)
        {
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Book_Images", Path.GetFileName(book.ImagePath));
            
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
            else
            {
                throw new Exception();
            }

            _db.Books.Remove(book);

            var rowsAffected = await _db.SaveChangesAsync();

            return rowsAffected < 0;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _db.Books.Include(b => b.Author)
                                  .Include(b => b.Genres)
                                    .ThenInclude(bk => bk.Genre)
                                  .ToListAsync();
        }

        public async Task<Book> GetBook(string bookId)
        {
            return await _db.Books.Include(b => b.Author)
                                  .Include(b => b.Genres)
                                    .ThenInclude(bk => bk.Genre)
                                  .FirstOrDefaultAsync(b => b.BookId == Guid.Parse(bookId));
        }

        public async Task<List<Book>> GetFilteredBooks(Expression<Func<Book, bool>> predicate)
        {
            return await _db.Books.Include(b => b.Author)
                                  .Include(b => b.Genres)
                                    .ThenInclude(bk => bk.Genre)
                                  .Where(predicate)
                                  .ToListAsync();
        }
    }
}
