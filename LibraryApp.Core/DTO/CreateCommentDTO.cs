using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.DTO
{
    public class CreateCommentDTO
    {
        public string PostId { get; set; }

        [StringLength(400, MinimumLength = 8)]
        [Required]
        public string Content { get; set; }
    }
}
