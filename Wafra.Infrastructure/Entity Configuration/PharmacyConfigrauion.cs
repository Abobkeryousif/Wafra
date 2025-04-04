using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wafra.Core.Entites;

namespace Wafra.Infrastructure.Entity_Configuration
{
    public class PharmacyConfigrauion : IEntityTypeConfiguration<Pharmacies>
    {
        public void Configure(EntityTypeBuilder<Pharmacies> builder)
        {
            builder.Property(x=> x.Name).IsRequired().HasMaxLength(50);
            builder.Property(op=> op.Phone).IsRequired().HasMaxLength(50);
            builder.HasMany(p => p.Medicines).WithMany(m => m.Pharmacies).UsingEntity<PharmacyMedicine>();
            
        }
    }
}
