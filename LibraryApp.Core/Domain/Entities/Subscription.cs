using LibraryApp.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.Domain.Entities
{
    public class Subscription
    {
        [Key]
        public Guid SubscriptionId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        [Required]
        public User User { get; set; }

        [Required]
        public Guid SubscriberId { get; set; }

        [ForeignKey("SubscriberId")]
        [Required]
        public User Subscriber { get; set; }
    }
}
