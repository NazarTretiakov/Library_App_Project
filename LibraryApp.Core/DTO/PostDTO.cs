using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.DTO
{
    public class PostDTO
    {
        [DataType(DataType.Text)]
        [Length(4, 40, ErrorMessage = "Title should have a length between 4 and 44 characters.")]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        [Length(2, 44, ErrorMessage = "Added to many topics.")]
        public string Topics { get; set; }

        [DataType(DataType.MultilineText)]
        [Length(140, 1700, ErrorMessage = "Content of the post should have a length between 140 and 1700 characters.")]
        public string Content { get; set; }
    }
}
