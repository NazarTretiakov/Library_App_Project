using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.DTO
{
    public class ReviewDTO
    {
        public string BookId { get; set; }

        [DataType(DataType.Text)]
        [Length(4, 50, ErrorMessage = "Title should have a length between 4 and 50 characters.")]
        public string Title { get; set; }

        [DataType(DataType.Text, ErrorMessage = "Rate doesn't match the correct format.")]
        [Range(1, 5, ErrorMessage = "Rating should be between 1 and 5.")]
        public int Rating { get; set; }

        [DataType(DataType.MultilineText)]
        [Length(40, 1000, ErrorMessage = "Content of the review should have a length between 140 and 1000 characters.")]
        public string Content { get; set; }
    }
}
