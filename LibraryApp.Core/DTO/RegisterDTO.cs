using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Username can't be blank")]
        [Length(8, 20, ErrorMessage = "Username should have a length between 8 and 20 characters.")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password can't be blank")]
        [Length(8, 20, ErrorMessage = "Password should have a length between 8 and 20 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
