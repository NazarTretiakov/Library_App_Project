using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class SavesRemoverService : ISavesRemoverService
    {
        private readonly ISavesRepository _savesRepository;

        public SavesRemoverService(ISavesRepository savesRepository)
        {
            _savesRepository = savesRepository;
        }

        public async Task<bool> RemoveSave(Save save)
        {
            return await _savesRepository.RemoveSave(save);
        }
    }
}
