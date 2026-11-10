using HobbyHub_CodeFirst1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HobbyHub_CodeFirst1.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
    

    public DbSet<Category> Categories { get; set; }

        public DbSet<Hobby> Hobbies { get; set; }

        public DbSet<Community> Communities { get; set; }

        public DbSet<IdeaTemplate> IdeaTemplates { get; set; }

        public DbSet<IdeaTemplateStep> IdeaTemplateSteps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Category
            modelBuilder.Entity<Category>().Property(c => c.Slug).HasMaxLength(80).IsRequired();
            modelBuilder.Entity<Category>().Property(c => c.Name).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Category>().Property(c => c.IconKey).HasMaxLength(100);
            modelBuilder.Entity<Category>().Property(c => c.TintColor).HasColumnType("char(7)").IsRequired();

            modelBuilder.Entity<Category>().HasAlternateKey(c => c.Slug);
            modelBuilder.Entity<Category>().HasAlternateKey(c => c.Name);

            modelBuilder.Entity<Category>().ToTable(t =>
                t.HasCheckConstraint("CK_Categories_SortOrder", "SortOrder >= 0"));


            // Hobby
            modelBuilder.Entity<Hobby>().Property(h => h.Slug).HasMaxLength(80).IsRequired();
            modelBuilder.Entity<Hobby>().Property(h => h.Name).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Hobby>().Property(h => h.Definition).HasMaxLength(2000).IsRequired();
            modelBuilder.Entity<Hobby>().Property(h => h.StarterStep).HasMaxLength(2000).IsRequired();
            modelBuilder.Entity<Hobby>().Property(h => h.StartingRequirements).HasMaxLength(2000).IsRequired();
            modelBuilder.Entity<Hobby>().Property(h => h.IconKey).HasMaxLength(100);

            modelBuilder.Entity<Hobby>().HasAlternateKey(h => h.Slug);
            modelBuilder.Entity<Hobby>().HasAlternateKey(h => h.Name);

            modelBuilder.Entity<Hobby>().ToTable(t =>
                t.HasCheckConstraint("CK_Hobbies_Name", "Name <> ''"));

            modelBuilder.Entity<Hobby>()
                .HasOne(h => h.Category)
                .WithMany(c => c.Hobbies)
                .HasForeignKey(h => h.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);


            // Community
            modelBuilder.Entity<Community>().Property(c => c.Name).HasMaxLength(150).IsRequired();
            modelBuilder.Entity<Community>().Property(c => c.Description).HasMaxLength(2000).IsRequired();
            modelBuilder.Entity<Community>().Property(c => c.Status).HasColumnType("varchar(10)").IsRequired();

            modelBuilder.Entity<Community>().HasAlternateKey(c => c.HobbyId);

            modelBuilder.Entity<Community>().ToTable(t =>
                t.HasCheckConstraint("CK_Communities_Status", "Status IN ('Active','Archived')"));

            modelBuilder.Entity<Community>()
                .HasOne(c => c.Hobby)
                .WithOne(h => h.Community)
                .HasForeignKey<Community>(c => c.HobbyId)
                .OnDelete(DeleteBehavior.NoAction);


            // IdeaTemplate
            modelBuilder.Entity<IdeaTemplate>().Property(i => i.Title).HasMaxLength(200).IsRequired();
            modelBuilder.Entity<IdeaTemplate>().Property(i => i.Description).HasMaxLength(2000).IsRequired();
            modelBuilder.Entity<IdeaTemplate>().Property(i => i.ExperienceLevel).HasColumnType("varchar(20)");
            modelBuilder.Entity<IdeaTemplate>().Property(i => i.PlaceText).HasMaxLength(300);

            modelBuilder.Entity<IdeaTemplate>().ToTable(t =>
                t.HasCheckConstraint("CK_IdeaTemplates_SuggestedMinutes",
                "SuggestedMinutes IS NULL OR SuggestedMinutes BETWEEN 5 AND 480"));

            modelBuilder.Entity<IdeaTemplate>()
                .HasOne(i => i.Hobby)
                .WithMany(h => h.IdeaTemplates)
                .HasForeignKey(i => i.HobbyId)
                .OnDelete(DeleteBehavior.NoAction);


            // IdeaTemplateStep
            modelBuilder.Entity<IdeaTemplateStep>().Property(s => s.Title).HasMaxLength(250).IsRequired();
            modelBuilder.Entity<IdeaTemplateStep>().Property(s => s.Description).HasMaxLength(2000).IsRequired();

            modelBuilder.Entity<IdeaTemplateStep>()
                .HasAlternateKey(s => new { s.TemplateId, s.SortOrder });

            modelBuilder.Entity<IdeaTemplateStep>().ToTable(t =>
                t.HasCheckConstraint("CK_IdeaTemplateSteps_SortOrder", "SortOrder >= 0"));

            modelBuilder.Entity<IdeaTemplateStep>()
                .HasOne(s => s.IdeaTemplate)
                .WithMany(i => i.IdeaTemplateSteps)
                .HasForeignKey(s => s.TemplateId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}




