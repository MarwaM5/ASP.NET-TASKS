using AcademyGraduationSupply.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AcademyGraduationSupply.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): IdentityDbContext(options)
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Offer> Offers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Offer>()
                .Property(o => o.UnitPrice)
                .HasPrecision(18, 2);
        }
    }




}
