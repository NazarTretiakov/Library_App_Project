using LibraryApp.Core.Domain.Entities;
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

        public async Task<List<User>> GetFilteredUsers(string searchFilter, string searchString)
        {
            List<User> filteredUsers = null;

            switch (searchFilter)
            {
                case "all":
                    filteredUsers = await _usersRepository.GetFilteredUsers(u => u.NormalizedUserName.Contains(searchString.ToUpper()) ||
                                                                                 u.Firstname.ToUpper().Contains(searchString.ToUpper()) ||
                                                                                 u.Lastname.ToUpper().Contains(searchString.ToUpper()));
                    break;

                case "username":
                    filteredUsers = await _usersRepository.GetFilteredUsers(u => u.NormalizedUserName.Contains(searchString.ToUpper()));
                    break;

                case "firstname":
                    filteredUsers = await _usersRepository.GetFilteredUsers(u => u.Firstname.ToUpper().Contains(searchString.ToUpper()));
                    break;

                case "lastname":
                    filteredUsers = await _usersRepository.GetFilteredUsers(u => u.Lastname.ToUpper().Contains(searchString.ToUpper()));
                    break;
            }

            return filteredUsers;
        }

        public async Task<User> GetUserByUserId(string userId)
        {
            return await _usersRepository.GetUser(userId);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _usersRepository.GetUserByUsername(username);
        }
    }
}
