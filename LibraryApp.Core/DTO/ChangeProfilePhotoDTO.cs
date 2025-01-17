using Microsoft.AspNetCore.Http;

namespace LibraryApp.Core.DTO
{
    public class ChangeProfilePhotoDTO
    {
        public IFormFile? Photo { get; set; }
    }
}
