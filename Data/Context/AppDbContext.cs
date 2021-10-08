using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired();

            modelBuilder.Entity<Brand>()
                .Property(c => c.Name)
                .IsRequired();

            modelBuilder.Entity<BrandCategory>()
                .HasKey(x => new { x.BrandId, x.CategoryId });

            modelBuilder.Entity<BrandCategory>()
                .HasOne(bc => bc.Brand)
                .WithMany(b => b.BrandsCategories)
                .HasForeignKey(bc => bc.BrandId);

            modelBuilder.Entity<BrandCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.BrandsCategories)
                .HasForeignKey(bc => bc.CategoryId);

            modelBuilder.Entity<Company>()
                .Property(c => c.Name)
                .IsRequired();


            modelBuilder.Entity<Company>()
                .HasMany(c => c.Brands)
                .WithOne(b => b.Company)
                .IsRequired();

            modelBuilder.Entity<Company>()
                .HasOne(c => c.Rating)
                .WithOne(r => r.Company)
                .IsRequired();


        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BrandCategory> BrandsCategories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
