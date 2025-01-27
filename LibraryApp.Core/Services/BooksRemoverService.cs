using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class BooksRemoverService : IBooksRemoverService
    {
        private readonly IBooksRepository _booksRepository;

        public BooksRemoverService(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<bool> DeleteBook(Book book)
        {
            return await _booksRepository.DeleteBook(book);
        }
    }
}
