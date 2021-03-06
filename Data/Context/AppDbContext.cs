using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

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


            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Company)
                .WithOne(c => c.Rating)
                .HasForeignKey<Company>(c => c.RatingId)
                .IsRequired();


            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Type)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Brands)
                .WithOne(b => b.Creator);


            modelBuilder.Entity<Request>()
                .HasOne(r => r.Brand)
                .WithOne(b => b.Request);

            modelBuilder.Entity<Request>()
                .Property(r => r.Name)
                .IsRequired();


            modelBuilder.Entity<RatingEntry>()
                .HasOne(re => re.Rating)
                .WithMany(r => r.RatingEntries);

            modelBuilder.Entity<RatingEntry>()
                .HasOne(re => re.User)
                .WithMany(u => u.Ratings);


            modelBuilder.Entity<Comment>()
                .Property(c => c.Text)
                .IsRequired();

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Entry)
                .WithOne(re => re.Comment);
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BrandCategory> BrandsCategories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RatingEntry> RatingEntries { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
