using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.DTO;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Core.Services
{
    public class ChangeProfilePhotoService : IChangeProfilePhotoService
    {
        private readonly IUsersRepository _usersRepository;

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public ChangeProfilePhotoService(IUsersRepository usersRepository, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _usersRepository = usersRepository;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<bool> ChangeProfilePhoto(ChangeProfilePhotoDTO changeProfilePhotoDTO)
        {
            string profilePhotosFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Profile_Photos");
            User currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (File.Exists(Path.Combine(profilePhotosFolder, currentUser.ProfilePhotoPath.Substring(24))))
            {
                File.Delete(Path.Combine(profilePhotosFolder, currentUser.ProfilePhotoPath.Substring(24)));
            }

            if (changeProfilePhotoDTO.Photo != null)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + changeProfilePhotoDTO.Photo.FileName;
                string photoPathToCreateFile = Path.Combine(profilePhotosFolder, uniqueFileName);

                using (var fileStream = new FileStream(photoPathToCreateFile, FileMode.Create))
                {
                    await changeProfilePhotoDTO.Photo.CopyToAsync(fileStream);
                }

                string photoPath = $"~/Images/Profile_Photos/{uniqueFileName}";

                await _usersRepository.ChangeUserProfilePhoto(currentUser, photoPath);
            }
            else
            {
                string photoPath = $"~/Images/Icons/User_Profile_Photo.svg";

                await _usersRepository.ChangeUserProfilePhoto(currentUser, photoPath);
            }
            return true;
        }
    }
}
