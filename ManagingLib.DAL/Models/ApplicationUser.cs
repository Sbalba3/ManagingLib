using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagingLib.DAL.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FName { get; set; }
        public string LName { get; set; }
        [Required]
        public bool IsAgree { get; set; }
    }
}
