using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HobbyHub_Management_System.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Community> Communities { get; set; }

    public virtual DbSet<Hobby> Hobbies { get; set; }

    public virtual DbSet<IdeaTemplate> IdeaTemplates { get; set; }

    public virtual DbSet<IdeaTemplateStep> IdeaTemplateSteps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=HobbyHub_DatabaseFirst;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0BB7725FE6");

            entity.HasIndex(e => e.Name, "UQ__Categori__737584F6171BDB8B").IsUnique();

            entity.HasIndex(e => e.Slug, "UQ__Categori__BC7B5FB60A6681D0").IsUnique();

            entity.Property(e => e.IconKey).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Slug).HasMaxLength(80);
            entity.Property(e => e.TintColor)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Community>(entity =>
        {
            entity.HasKey(e => e.CommunityId).HasName("PK__Communit__CCAA5B69B6B27478");

            entity.HasIndex(e => e.HobbyId, "UQ__Communit__0ABE0BCEF571822D").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Hobby).WithOne(p => p.Community)
                .HasForeignKey<Community>(d => d.HobbyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Communiti__Hobby__5629CD9C");
        });

        modelBuilder.Entity<Hobby>(entity =>
        {
            entity.HasKey(e => e.HobbyId).HasName("PK__Hobbies__0ABE0BCFABE0115D");

            entity.HasIndex(e => e.Name, "UQ__Hobbies__737584F690956949").IsUnique();

            entity.HasIndex(e => e.Slug, "UQ__Hobbies__BC7B5FB693C22DA5").IsUnique();

            entity.Property(e => e.Definition).HasMaxLength(2000);
            entity.Property(e => e.IconKey).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Slug).HasMaxLength(80);
            entity.Property(e => e.StarterStep).HasMaxLength(2000);
            entity.Property(e => e.StartingRequirements).HasMaxLength(2000);

            entity.HasOne(d => d.Category).WithMany(p => p.Hobbies)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Hobbies__Categor__5165187F");
        });

        modelBuilder.Entity<IdeaTemplate>(entity =>
        {
            entity.HasKey(e => e.TemplateId).HasName("PK__IdeaTemp__F87ADD27858EB782");

            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.ExperienceLevel)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PlaceText).HasMaxLength(300);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Hobby).WithMany(p => p.IdeaTemplates)
                .HasForeignKey(d => d.HobbyId)
                .HasConstraintName("FK__IdeaTempl__Hobby__59FA5E80");
        });

        modelBuilder.Entity<IdeaTemplateStep>(entity =>
        {
            entity.HasKey(e => e.TemplateStepId).HasName("PK__IdeaTemp__6772FDCA3895AA8B");

            entity.HasIndex(e => new { e.TemplateId, e.SortOrder }, "UQ__IdeaTemp__8D20C41DA5614BD8").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Title).HasMaxLength(250);

            entity.HasOne(d => d.Template).WithMany(p => p.IdeaTemplateSteps)
                .HasForeignKey(d => d.TemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__IdeaTempl__Templ__5EBF139D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
