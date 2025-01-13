using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Core.Services
{
    public class ChangeProfileInformationService : IChangeProfileInformationService
    {
        private readonly IUsersGetterService _usersGetterService;
        private readonly IUsersRepository _usersRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public ChangeProfileInformationService(IUsersGetterService usersGetterService, IUsersRepository usersRepository, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _usersGetterService = usersGetterService;
            _usersRepository = usersRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<User> ChangeProfileInformation(ChangeProfileInformationDTO changeProfileInformationDTO)
        {
            User currentWorkingUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            currentWorkingUser = await _usersRepository.ChangeUserInformation(currentWorkingUser, changeProfileInformationDTO);

            return currentWorkingUser;
        }
    }
}
