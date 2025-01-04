using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class LikesGetterService : ILikesGetterService
    {
        private readonly ILikesRepository _likesRepository;

        public LikesGetterService(ILikesRepository likesRepository)
        {
            _likesRepository = likesRepository;
        }

        public async Task<Like> GetLikeByUserIdAndPostId(string userId, string postId)
        {
            return await _likesRepository.GetLike(userId, postId);
        }

        public async Task<List<Like>> GetPostLikes(string postId)
        {
            return await _likesRepository.GetPostLikes(postId);
        }
    }
}
