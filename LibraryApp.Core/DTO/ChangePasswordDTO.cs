using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.DTO
{
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage = "Password can't be blank")]
        [Length(8, 20, ErrorMessage = "Password should have a length between 8 and 20 characters.")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password can't be blank")]
        [Length(8, 20, ErrorMessage = "Password should have a length between 8 and 20 characters.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Length(8, 20, ErrorMessage = "Password should have a length between 8 and 20 characters.")]
        [Compare("NewPassword", ErrorMessage = "Entered passwords don't match.")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}
