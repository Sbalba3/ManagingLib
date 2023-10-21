using ManagingLib.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagingLib.DAL.Context.Config
{
    internal class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasOne(B => B.Author).WithMany(A => A.Books).HasForeignKey(B => B.AuthorId);
            builder.HasOne(B => B.Genre).WithMany(G => G.Books).HasForeignKey(B => B.GenreId);
            builder.Property(B=>B.Title).IsRequired().HasMaxLength(100);
            builder.Property(B => B.PublicationYear).IsRequired();
        }
    }
}
