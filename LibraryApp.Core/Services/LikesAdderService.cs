using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class LikesAdderService : ILikesAdderService
    {
        private readonly ILikesRepository _likesRepository;
        private readonly ILikesGetterService _likesGetterService;

        public LikesAdderService(ILikesRepository likesRepository, ILikesGetterService likesGetterService)
        {
            _likesRepository = likesRepository;
            _likesGetterService = likesGetterService;
        }

        public async Task<Like> AddLike(Post post, User user)
        {
            bool result = await _likesRepository.AddLike(post, user);

            if (result)
            {
                return await _likesGetterService.GetLikeByUserIdAndPostId(user.Id.ToString(), post.PostId.ToString());
            }
            else
            {
                throw new Exception("Error while adding like to db.");
            }
        }
    }
}
