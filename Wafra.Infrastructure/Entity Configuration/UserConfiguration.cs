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
    public class UserConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Phone).IsRequired();
            builder.Property(n => n.Name).HasMaxLength(255).IsRequired();
            builder.Property(n => n.Email).HasMaxLength(155).IsRequired();
            builder.HasMany(t=> t.refreshTokens).WithOne(u=> u.Users).HasForeignKey(u=> u.userId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
