using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.Domain.Entities
{
    public class Author
    {
        [Key]
        public Guid AuthorId { get; set; }

        [StringLength(40, MinimumLength = 4)]
        [Required]
        public string Firstname { get; set; }

        [StringLength(40, MinimumLength = 4)]
        [Required]
        public string Lastname { get; set; }

        [StringLength(500, MinimumLength = 4)]
        [Required]
        public string Description { get; set; }

        public List<Book>? Books { get; set; }
    }
}
