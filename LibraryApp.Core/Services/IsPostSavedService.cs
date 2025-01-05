using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Core.Services
{
    public class IsPostSavedService : IIsPostSavedService
    {
        private readonly ISavesGetterService _savesGetterService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public IsPostSavedService(ISavesGetterService savesGetterService, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _savesGetterService = savesGetterService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<bool> IsPostSaved(string postId)
        {
            User currentWorkingUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            Save saveOfPost = await _savesGetterService.GetSaveByUserIdAndObjectId(currentWorkingUser.Id.ToString(), postId);

            return saveOfPost != null;
        }
    }
}
