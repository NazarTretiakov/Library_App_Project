using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Core.Services
{
    public class IsPostLikedService : IIsPostLikedService
    {
        private readonly ILikesGetterService _likesGetterService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public IsPostLikedService(ILikesGetterService likesGetterService, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _likesGetterService = likesGetterService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<bool> IsPostLiked(string postId)
        {
            User currentWorkingUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            Like likeOfPost = await _likesGetterService.GetLikeByUserIdAndPostId(currentWorkingUser.Id.ToString(), postId);

            return likeOfPost != null;
        }
    }
}
