using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class LikesRemoverService : ILikesRemoverService
    {
        private readonly ILikesRepository _likesRepository;

        public LikesRemoverService(ILikesRepository likesRepository)
        {
            _likesRepository = likesRepository;
        }

        public async Task<bool> RemoveLike(Like like)
        {
            return await _likesRepository.RemoveLike(like);
        }
    }
}
