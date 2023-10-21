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
    internal class GenreConfig : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasMany(G => G.Books).WithOne(B => B.Genre);
            builder.Property(G=>G.Name).IsRequired().HasMaxLength(50);
            builder.Property(G => G.Description).IsRequired();

        }
    }
}
