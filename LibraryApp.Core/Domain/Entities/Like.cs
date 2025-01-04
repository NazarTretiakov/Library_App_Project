using LibraryApp.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Core.Domain.Entities
{
    public class Like
    {
        [Key]
        public Guid LikeId { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
        public Guid PostId { get; set; }

        [ForeignKey("PostId")]
        public Post? Post { get; set; }
    }
}
