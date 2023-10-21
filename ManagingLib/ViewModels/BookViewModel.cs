using ManagingLib.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ManagingLib.ViewModels
{
    public class BookViewModel
    {
        [Key]
        public int Id { get; set; }
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Title is required!")]
        [MaxLength(50, ErrorMessage = "max length is 50 char")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Publication Year is required!")]
        [DataType(DataType.Date)]
        public DateTime PublicationYear { get; set; }
        //public bool IsDeleted { get; set; } soft Delete
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
        [ForeignKey("Genre")]
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}
