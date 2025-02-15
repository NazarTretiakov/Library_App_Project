using LibraryApp.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Core.Domain.Entities
{
    public class Message
    {
        [Key]
        public Guid MessageId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [StringLength(1000, MinimumLength = 20)]
        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime DateOfPublication { get; set; }
    }
}
