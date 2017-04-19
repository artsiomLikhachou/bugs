using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestEfMemory;

namespace TestEfMemory
{
    public class TestDbContext: DbContext
    {
        public TestDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }

        public DbSet<Vendor> Vendor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasOne(x => x.Vendor)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.VendorId);
        }
    }

    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Data { get; set; }

        public string Prop1 { get; set; }

        public string Prop2 { get; set; }

        public virtual Vendor Vendor { get; set; }

        public Guid VendorId { get; set; }
    }

    public class Vendor
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
