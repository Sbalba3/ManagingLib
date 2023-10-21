using ManagingLib.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace ManagingLib.ViewModels
{
    public class GenreViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        [MaxLength(50, ErrorMessage = "max length is 50 char")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; }

        public ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}
