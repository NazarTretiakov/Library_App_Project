using LibraryApp.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Core.Domain.Entities
{
    public class Post
    {
        [Key]
        public Guid PostId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [StringLength(40, MinimumLength = 4)]
        [Required]
        public string Title { get; set; }

        public List<PostTopic>? Topics { get; set; }

        [StringLength(1700, MinimumLength = 20)]
        [Required]
        public string Content { get; set; }

        public List<Like>? Likes { get; set; }

        public List<Save>? Saves { get; set; }

        [Required]
        public DateTime DateOfPublication { get; set; }

        public List<Comment>? Comments { get; set; }
    }
}
