using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.Domain.Entities
{
    public class PostTopic
    {
        [Key]
        public Guid PostTopicId { get; set; }

        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public Guid TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}
