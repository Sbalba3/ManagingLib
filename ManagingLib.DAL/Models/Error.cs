using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagingLib.DAL.Models
{
    public class Error
    {
        [Key]
        public int Id { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        public DateTime DateTime { get; set; }
        public string Message { get; set; }
    }
}
