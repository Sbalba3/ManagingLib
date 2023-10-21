using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagingLib.DAL.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }

       public ICollection<Book> Books { get; set; }= new HashSet<Book>();

    }
}
