﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Wafra.Core.Entites;

namespace Wafra.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Users> Users { get; set; }  
        public virtual DbSet<Pharmacies> Pharmacies { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Medicines> Medicines { get; set; }

        //public virtual DbSet<Pharmacy>

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }

        public virtual DbSet<PharmacyMedicine> PharmacyMedicines{ get; set; }  

        public virtual DbSet<OTP> OTPs { get; set; }

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public virtual DbSet<Verification> Verifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
