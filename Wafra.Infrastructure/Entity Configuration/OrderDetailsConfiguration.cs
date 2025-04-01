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
    public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>

    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.HasOne(od=> od.Medicine).WithMany().HasForeignKey(od=> od.MedicineId);
        }
    }
}
