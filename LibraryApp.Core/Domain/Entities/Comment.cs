using LibraryApp.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Core.Domain.Entities
{
    public class Comment
    {
        [Key]
        public Guid CommentId { get; set; }


        [ForeignKey("UserId")]
        [Required]
        public User? User { get; set; }

        [ForeignKey("PostId")]
        [Required]
        public Post? Post { get; set; }

        [StringLength(400, MinimumLength = 8)]
        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime DateOfPublication { get; set; }
    }
}
