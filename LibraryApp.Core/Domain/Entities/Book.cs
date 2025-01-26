using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Core.Domain.Entities
{
    public class Book
    {
        [Key]
        public Guid BookId { get; set; }

        [ForeignKey("AuthorId")]
        public Author? Author { get; set; }

        [StringLength(30)]
        public string? ImagePath { get; set; }

        [StringLength(50, MinimumLength = 4)]
        [Required]
        public string Title { get; set; }

        [Range(0, 2500)]
        [Required]
        public int PublicationYear { get; set; }

        public List<BookGenre>? Genres { get; set; }

        [Range(0, 5)]
        [Required]
        public double Rating { get; set; }

        [StringLength(30, MinimumLength = 4)]
        [Required]
        public string Language { get; set; }

        [StringLength(700, MinimumLength = 4)]
        [Required]
        public string Description { get; set; }

        [Range(0, 100)]
        [Required]
        public int Amount { get; set; }

        [Range(0, 100)]
        [Required]
        public int Holds { get; set; }

        public List<Save>? Saves { get; set; }
    }
}
