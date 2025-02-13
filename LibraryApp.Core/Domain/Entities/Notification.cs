using LibraryApp.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Core.Domain.Entities
{
    public class Notification
    {
        [Key]
        public Guid NotificationId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("BookId")]
        public Book? Book { get; set; }

        [ForeignKey("NotificationReceiverId")]
        [Required]
        public User NotificationReceiver { get; set; }

        [StringLength(200, MinimumLength = 4)]
        [Required]
        public string Content { get; set; }
        public string NotificationType { get; set; }

        [Required]
        public DateTime DateOfCreation { get; set; }
    }
}
