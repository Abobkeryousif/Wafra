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
    public class OtpConfiguration : IEntityTypeConfiguration<OTP>
    {
        public void Configure(EntityTypeBuilder<OTP> builder)
        {
            builder.Property(o=> o.Otp).HasMaxLength(6).IsRequired();
            builder.Property(o=> o.UserEmail).IsRequired().HasMaxLength(200);
            builder.HasKey(o => o.Id);
        }
    }
}
