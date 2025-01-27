using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class ChangeBookAmountService : IChangeBookAmountService
    {
        private readonly IBooksRepository _booksRepository;

        public ChangeBookAmountService(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<Book> ChangeBookAmount(Book book, int newAmount)
        {
            return await _booksRepository.ChangeBookAmount(book, newAmount);
        }
    }
}
