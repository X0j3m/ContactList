using Contacts.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Contacts.Data
{
    public class ContactsDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }

        public ContactsDbContext(DbContextOptions<ContactsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Contact
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(c => c.Surname)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(c => c.Email)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.HasIndex(c => c.Email)
                      .IsUnique();

                entity.Property(c => c.Password)
                      .IsRequired();

                entity.Property(c => c.Phone)
                      .HasMaxLength(50);

                // Map DateOnly to DateTime in the database
                var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
                    d => d.ToDateTime(TimeOnly.MinValue),
                    dt => DateOnly.FromDateTime(dt));

                entity.Property(c => c.BirthDate)
                      .HasConversion(dateOnlyConverter);

                // Configure optional relationships using shadow foreign keys
                entity.HasOne(c => c.Category)
                      .WithMany()
                      .HasForeignKey("CategoryId")
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(c => c.SubCategory)
                      .WithMany()
                      .HasForeignKey("SubCategoryId")
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasMany(c => c.SubCategories)
                      .WithOne(sc => sc.Category)
                      .HasForeignKey(sc => sc.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // SubCategory
            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.HasKey(sc => sc.Id);

                entity.Property(sc => sc.Name)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
