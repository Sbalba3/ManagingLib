using ManagingLib.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ManagingLib.DAL.Context
{
    public class ManagingContext: IdentityDbContext<ApplicationUser>
    {
        public ManagingContext(DbContextOptions options):base(options) 
        {
            
        }
      
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Error> Errors { get; set; }
    }
}
