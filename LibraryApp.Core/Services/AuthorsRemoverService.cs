using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class AuthorsRemoverService : IAuthorsRemoverService
    {
        private readonly IAuthorsRepository _authorsRepository;

        public AuthorsRemoverService(IAuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        public async Task<bool> DeleteAuthor(Author author)
        {
            return await _authorsRepository.DeleteAuthor(author);
        }
    }
}
