using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.Domain.Entities
{
    public class Book
    {
        [Key]
        public Guid BookId { get; set; }
        public List<Save>? Saves { get; set; }
    }
}
