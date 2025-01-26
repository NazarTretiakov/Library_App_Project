using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.Domain.Entities
{
    public class Genre
    {
        [Key]
        public Guid GenreId { get; set; }

        [StringLength(40, MinimumLength = 4)]
        [Required]
        public string Name { get; set; }

        public List<BookGenre>? Books { get; set; }
    }
}
