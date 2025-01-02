using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;

namespace LibraryApp.Core.Services
{
    public class UsersGetterService : IUsersGetterService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersGetterService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _usersRepository.GetAllUsers();
        }

        public async Task<User> GetUserByUserId(string userId)
        {
            return await _usersRepository.GetUser(userId);
        }
    }
}
