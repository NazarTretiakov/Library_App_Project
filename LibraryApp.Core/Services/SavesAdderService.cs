using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class SavesAdderService : ISavesAdderService
    {
        private readonly ISavesRepository _savesRepository;

        public SavesAdderService(ISavesRepository savesRepository)
        {
            _savesRepository = savesRepository;
        }

        public async Task<Save> AddBookSave(Book book, User user)
        {
            bool result = await _savesRepository.AddBookSave(book, user);

            if (result)
            {
                return await _savesRepository.GetSave(user.Id.ToString(), book.BookId.ToString());
            }
            else
            {
                throw new Exception("Save wasn't added to db.");
            }
        }

        public async Task<Save> AddPostSave(Post post, User user)
        {
            bool result = await _savesRepository.AddPostSave(post, user);

            if (result)
            {
                return await _savesRepository.GetSave(user.Id.ToString(), post.PostId.ToString());
            }
            else
            {
                throw new Exception("Save wasn't added to db.");
            }
        }
    }
}
