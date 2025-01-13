using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.DTO
{
    public class ChangeProfileInformationDTO
    {
        [Length(2, 12, ErrorMessage = "Firstname should have a length between 2 and 12 characters.")]
        [DataType(DataType.Text)]
        public string? Firstname{ get; set; }

        [Length(2, 12, ErrorMessage = "Lastname should have a length between 2 and 12 characters.")]
        [DataType(DataType.Text)]
        public string? Lastname { get; set; }

        [Length(0, 300, ErrorMessage = "Description should have maximum 300 characters.")]
        [DataType(DataType.Text)]
        public string? Description { get; set; }
    }
}
