using ManagingLib.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace ManagingLib.ViewModels
{
    public class AuthorViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        [MaxLength(50, ErrorMessage = "max length is 50 char")]
        public string Name { get; set; }
        [Required(ErrorMessage = "BirthDate is required!")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Nationality is required!")]
        [MaxLength(50, ErrorMessage = "max length is 50 char")]
        public string Nationality { get; set; }

        public ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}
