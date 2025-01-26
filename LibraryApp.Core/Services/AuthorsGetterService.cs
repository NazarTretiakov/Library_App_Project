using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class AuthorsGetterService : IAuthorsGetterService
    {
        private readonly IAuthorsRepository _authorsResository;

        public AuthorsGetterService(IAuthorsRepository authorsResository)
        {
            _authorsResository = authorsResository;
        }

        public Task<List<Author>> GetAllAuthors()
        {
            return _authorsResository.GetAllAuthors();
        }

        public Task<Author> GetAuthorByFullName(string firstname, string lastname)
        {
            return _authorsResository.GetAuthor(firstname, lastname);
        }
    }
}
