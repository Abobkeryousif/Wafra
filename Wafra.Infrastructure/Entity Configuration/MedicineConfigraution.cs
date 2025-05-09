﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wafra.Core.Entites;

namespace Wafra.Infrastructure.Entity_Configuration
{
    public class MedicineConfigraution : IEntityTypeConfiguration<Medicines>
    {
        public void Configure(EntityTypeBuilder<Medicines> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x=> x.Price).IsRequired();
            builder.HasOne(m => m.Category).WithMany(c => c.Medicines).HasForeignKey(m => m.CategoryId);
            
            
        }
    }
}
