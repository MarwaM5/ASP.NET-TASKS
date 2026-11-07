using CodeFirst.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Set decimal precision for Product Price
            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(10, 2);

            // Set decimal precision for PurchaseOrderItem UnitPrice
            builder.Entity<PurchaseOrderItem>()
                .Property(p => p.UnitPrice)
                .HasPrecision(10, 2);

            // Configure PurchaseOrderItem relationship with PurchaseOrder
            builder.Entity<PurchaseOrderItem>()
            .HasOne(p => p.PurchaseOrder)
            .WithMany(o => o.Items)
            .HasForeignKey(p => p.PurchaseOrderId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
