using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class BooksGetterService : IBooksGetterService
    {
        private readonly IBooksRepository _booksRepository;
        public BooksGetterService(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _booksRepository.GetAllBooks();
        }

        public async Task<Book> GetBookByBookId(string bookId)
        {
            return await _booksRepository.GetBook(bookId);
        }

        public async Task<List<Book>> GetFilteredBooks(string searchFilter, string searchString)
        {
            List<Book> filteredBooks = null;

            switch (searchFilter)
            {
                case "all":
                    filteredBooks = await _booksRepository.GetFilteredBooks(b => b.Title.ToUpper().Contains(searchString.ToUpper()) ||
                                                                                 (b.Author.Firstname.ToUpper() + " " + b.Author.Lastname.ToUpper()).Contains(searchString.ToUpper()) ||
                                                                                 b.Genres.Any(genre => genre.Genre.Name.ToUpper() == searchString.ToUpper()));
                    break;

                case "authorName":
                    filteredBooks = await _booksRepository.GetFilteredBooks(b => (b.Author.Firstname.ToUpper() + " " + b.Author.Lastname.ToUpper()).Contains(searchString.ToUpper()));
                    break;

                case "genre":
                    filteredBooks = await _booksRepository.GetFilteredBooks(b => b.Genres.Any(genre => genre.Genre.Name.ToUpper() == searchString.ToUpper()));
                    break;

                case "title":
                    filteredBooks = await _booksRepository.GetFilteredBooks(b => b.Title.ToUpper().Contains(searchString.ToUpper()));
                    break;
            }

            return filteredBooks;
        }
    }
}
