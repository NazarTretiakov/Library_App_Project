using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Core.Services
{
    public class ToggleLikeService : IToggleLikeService
    {
        private readonly IPostsGetterService _postsGettersService;
        private readonly ILikesGetterService _likesGetterService;
        private readonly ILikesRemoverService _likesRemoverService;
        private readonly ILikesAdderService _likesAdderService;
        private readonly INotificationsCreatorService _notificationsCreatorService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public ToggleLikeService(IPostsGetterService postsGetterService, ILikesGetterService likesGetterService, ILikesRemoverService likesRemoverService, ILikesAdderService likesAdderService, IHttpContextAccessor httpContextAccessor, INotificationsCreatorService notificationsCreatorService, UserManager<User> userManager)
        {
            _postsGettersService = postsGetterService;
            _likesGetterService = likesGetterService;
            _likesRemoverService = likesRemoverService;
            _likesAdderService = likesAdderService;
            _httpContextAccessor = httpContextAccessor;
            _notificationsCreatorService = notificationsCreatorService;
            _userManager = userManager;
        }
        public async Task<bool> ToggleLike(string postId)
        {
            Post post = await _postsGettersService.GetPostByPostId(postId);

            if (post == null)
            {
                throw new ArgumentNullException();
            }

            User user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            Like likeOfUser = await _likesGetterService.GetLikeByUserIdAndPostId(user.Id.ToString(), post.PostId.ToString());

            if (likeOfUser != null)
            {
                await _likesRemoverService.RemoveLike(likeOfUser);

                return false;
            }
            else
            {
                await _likesAdderService.AddLike(post, user);

                await _notificationsCreatorService.CreateNotification(new NotificationDTO()
                {
                    ObjectId = user.Id.ToString(),
                    ReceiverId = post.User.Id.ToString(),
                    Content = $"{user.UserName} has liked your post \"{post.Title}\"."
                });

                return true;
            }
        }
    }
}
