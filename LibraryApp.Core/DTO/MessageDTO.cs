using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.DTO
{
    public class MessageDTO
    {
        [DataType(DataType.MultilineText)]
        [Length(20, 1000, ErrorMessage = "Content of the post should have a length between 140 and 1000 characters.")]
        public string Content { get; set; }
    }
}
