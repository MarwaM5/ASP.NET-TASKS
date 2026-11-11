using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniversityCafeteriaSystem_Identity1.Models;

namespace UniversityCafeteriaSystem_Identity1.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<SupplyRequest> SupplyRequests { get; set; }

        public DbSet<RequestItem> RequestItems { get; set; }
    }

}
