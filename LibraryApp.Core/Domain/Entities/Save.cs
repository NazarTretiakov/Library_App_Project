using LibraryApp.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Core.Domain.Entities
{
    public class Save
    {
        [Key]
        public Guid SaveId { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
        public Guid? PostId { get; set; }

        [ForeignKey("PostId")]
        public Post? Post { get; set; }
        public Guid? BookId { get; set; }

        [ForeignKey("BookId")]
        public Book? Book { get; set; }
    }
}
