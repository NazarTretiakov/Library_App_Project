using LibraryApp.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Core.Domain.Entities
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        [ForeignKey("UserId")]
        [Required]
        public User? User { get; set; }

        [ForeignKey("BookId")]
        [Required]
        public Book? Book { get; set; }
        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime DateOfOrder { get; set; }

        [Required]
        public bool IsChecked { get; set; }
    }
}
