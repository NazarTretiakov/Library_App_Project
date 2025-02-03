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

        public async Task<List<Author>> GetAllAuthors()
        {
            return await _authorsResository.GetAllAuthors();
        }

        public async Task<Author> GetAuthorByAuthorId(string authorId)
        {
            return await _authorsResository.GetAuthor(authorId);
        }

        public async Task<Author> GetAuthorByFullName(string firstname, string lastname)
        {
            return await _authorsResository.GetAuthor(firstname, lastname);
        }

        public async Task<List<Author>> GetFilteredAuthors(string searchFilter, string searchString)
        {
            List<Author> filteredAuthors = null;

            switch (searchFilter)
            {
                case "all":
                    filteredAuthors = await _authorsResository.GetFilteredAuthors(a => (a.Firstname.ToUpper() + " " + a.Lastname.ToUpper()).Contains(searchString.ToUpper()));
                    break;

                case "firstname":
                    filteredAuthors = await _authorsResository.GetFilteredAuthors(a => a.Firstname.ToUpper().Contains(searchString.ToUpper()));
                    break;

                case "lastname":
                    filteredAuthors = await _authorsResository.GetFilteredAuthors(a => a.Lastname.ToUpper().Contains(searchString.ToUpper()));
                    break;
            }

            return filteredAuthors;
        }
    }
}
