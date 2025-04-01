using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wafra.Core.Entites;

namespace Wafra.Infrastructure.Entity_Configuration
{
    public class OrderConfigarution : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(o => o.Users).WithMany(u => u.Orders).HasForeignKey(o => o.UserID);
            builder.HasOne(m=> m.Pharmacy).WithMany(p => p.Orders).HasForeignKey(x=> x.PharmacyID);
            builder.HasMany(o => o.OrderDetails).WithOne(od => od.Order).HasForeignKey(o => o.OrderId);
        }
    }
}
