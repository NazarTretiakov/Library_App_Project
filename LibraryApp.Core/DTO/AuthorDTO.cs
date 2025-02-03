using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.DTO
{
    public class AuthorDTO
    {
        [DataType(DataType.Text)]
        [Length(4, 40, ErrorMessage = "Firsname should have a length between 4 and 40 characters.")]
        public string Firstname { get; set; }

        [DataType(DataType.Text)]
        [Length(4, 40, ErrorMessage = "Lastname should have a length between 4 and 40 characters.")]
        public string Lastname { get; set; }

        [DataType(DataType.MultilineText)]
        [Length(4, 500, ErrorMessage = "Description of the book should have a length between 4 and 500 characters.")]
        public string Description { get; set; }
    }
}
