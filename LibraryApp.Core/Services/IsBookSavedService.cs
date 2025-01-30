using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Core.Services
{
    public class IsBookSavedService : IIsBookSavedService
    {
        private readonly ISavesGetterService _savesGetterService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public IsBookSavedService(ISavesGetterService savesGetterService, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _savesGetterService = savesGetterService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<bool> IsBookSaved(string bookId)
        {
            User currentWorkingUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            Save saveOfBook = await _savesGetterService.GetSaveByUserIdAndObjectId(currentWorkingUser.Id.ToString(), bookId);

            return saveOfBook != null;
        }
    }
}
