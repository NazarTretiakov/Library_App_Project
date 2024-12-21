using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Username can't be blank")]
        [StringLength(20, ErrorMessage = "Username has to be at least 8 characters long", MinimumLength = 8)]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password can't be blank")]
        [StringLength(20, ErrorMessage = "Password has to be at least 8 characters long", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
