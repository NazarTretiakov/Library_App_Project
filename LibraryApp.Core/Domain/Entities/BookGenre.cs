using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.Domain.Entities
{
    public class BookGenre
    {
        [Key]
        public Guid BookGenreId { get; set; }

        public Guid BookId { get; set; }
        public Book Book { get; set; }

        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
