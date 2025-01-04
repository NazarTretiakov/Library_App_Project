using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class LikesAdderService : ILikesAdderService
    {
        private readonly ILikesRepository _likesRepository;

        public LikesAdderService(ILikesRepository likesRepository)
        {
            _likesRepository = likesRepository;
        }

        public async Task<Like> AddLike(Post post, User user)
        {
            bool result = await _likesRepository.AddLike(post, user);

            if (result)
            {
                return await _likesRepository.GetLike(user.Id.ToString(), post.PostId.ToString());
            }
            else
            {
                throw new Exception("Like wasn't added to db.");
            }
        }
    }
}
