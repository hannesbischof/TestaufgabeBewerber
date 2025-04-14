using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(p => p.Price)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)");
                entity.Property(p => p.Description)
                      .IsRequired()
                      .HasMaxLength(500);
                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey("CategoryId")
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(p => p.Manufacturer)
                      .WithMany(m => m.Products)
                      .HasForeignKey(p => p.ManufacturerId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Category entity
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(c => c.Description)
                      .IsRequired()
                      .HasMaxLength(200);
            });

            // Configure Manufacturer entity
            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Name)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(m => m.Description)
                      .IsRequired()
                      .HasMaxLength(500);
            });
        }
    }
}