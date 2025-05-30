using CraftIQ.Inventory.Core.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Infrastructure.Data.Config
{
    public class CategoryConfiguration  : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(p => p.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p._CategoryId)
                    .IsRequired();

            builder.Property(p => p.Name)
                   .HasMaxLength(50);

            builder.Property(p => p.Description)
                   .HasMaxLength(200);
        }
    }
}
