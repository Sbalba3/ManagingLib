using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagingLib.DAL.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string ISBN { get; set; }

        public string Title { get; set; }
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
