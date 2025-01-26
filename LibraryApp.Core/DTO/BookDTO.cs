using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.DTO
{
    public class BookDTO
    {
        [DataType(DataType.Text)]
        [Length(4, 50, ErrorMessage = "Title should have a length between 4 and 50 characters.")]
        public string Title { get; set; }

        public IFormFile Image { get; set; }

        [DataType(DataType.Text)]
        [Length(4, 40, ErrorMessage = "Author's firstname should have a length between 4 and 40 characters.")]
        public string AuthorFirstname { get; set; }

        [DataType(DataType.Text)]
        [Length(4, 40, ErrorMessage = "Author's lastname should have a length between 4 and 40 characters.")]
        public string AuthorLastname { get; set; }

        [DataType(DataType.Text, ErrorMessage = "Publication year is not in the correct format")]
        [Range(4, 2500, ErrorMessage = "Entered incorrect publication year.")]
        public int PublicationYear { get; set; }

        [DataType(DataType.Text)]
        [Length(2, 44, ErrorMessage = "Added to many genres.")]
        public string Genres { get; set; }

        [DataType(DataType.Text)]
        [Length(4, 30, ErrorMessage = "Language should have a length between 4 and 30 characters.")]
        public string Language { get; set; }

        [DataType(DataType.MultilineText)]
        [Length(140, 1000, ErrorMessage = "Description of the book should have a length between 140 and 1000 characters.")]
        public string Description { get; set; }
    }
}
