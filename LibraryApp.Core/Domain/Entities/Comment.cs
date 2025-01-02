using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.Domain.Entities
{
    public class Comment
    {
        [Key]
        public Guid CommentId { get; set; }
    }
}
