using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class AuthorsAdderService : IAuthorsAdderService
    {
        private readonly IAuthorsRepository _authorsResository;

        public AuthorsAdderService(IAuthorsRepository authorsResository)
        {
            _authorsResository = authorsResository;
        }

        public async Task<Author> AddAuthor(AuthorDTO authorDTO)
        {
            Author newAuthor = new Author()
            {
                AuthorId = Guid.NewGuid(),
                Firstname = authorDTO.Firstname,
                Lastname = authorDTO.Lastname,
                Description = authorDTO.Description
            };

            await _authorsResository.AddAuthor(newAuthor);

            return newAuthor;
        }
    }
}
