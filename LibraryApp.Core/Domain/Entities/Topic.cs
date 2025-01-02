using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.Domain.Entities
{
    public class Topic
    {
        [Key]
        public Guid TopicId { get; set; }

        [StringLength(40, MinimumLength = 4)]
        [Required]
        public string Name { get; set; }

        public List<PostTopic>? Posts { get; set; }
    }
}
