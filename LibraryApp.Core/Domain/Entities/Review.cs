using LibraryApp.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.Domain.Entities
{
    public class Review
    {
        [Key]
        public Guid ReviewId { get; set; }

        [ForeignKey("UserId")]
        [Required]
        public User? User { get; set; }

        [ForeignKey("BookId")]
        [Required]
        public Book? Book { get; set; }

        [StringLength(50, MinimumLength = 4)]
        [Required]
        public string Title { get; set; }

        [Range(0, 5)]
        [Required]
        public byte Rating { get; set; }

        [StringLength(1000, MinimumLength = 40)]
        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime DateOfPublication { get; set; }
    }
}
