using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.DTO
{
    public class SignInDTO
    {
        [Required(ErrorMessage = "Username can't be blank")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password can't be blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
