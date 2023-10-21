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
    internal class AuthorConfig : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasMany(A => A.Books).WithOne(B => B.Author);
            builder.Property(A=>A.BirthDate).IsRequired();
            builder.Property(A => A.Name).IsRequired().HasMaxLength(50);
            builder.Property(A => A.Nationality).IsRequired().HasMaxLength(50);



        }
    }
}
