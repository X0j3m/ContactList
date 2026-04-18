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

                entity.HasData(
                    new Category { Id = Guid.Parse("462cdacf-1d2e-49d7-af10-aefad2d29459"), Name = "Służbowy"},
                    new Category { Id = Guid.Parse("020b977f-e9f0-4310-af8e-1be21fab0a77"), Name = "Prywatny"}
                    );
            });

            // SubCategory
            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.HasKey(sc => sc.Id);

                entity.Property(sc => sc.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                // Seed some subcategories linked to the seeded categories
                entity.HasData(
                    new SubCategory { Id = Guid.Parse("b1a7c6d8-3f44-4c9a-9f1a-111111111111"), Name = "Szef", CategoryId = Guid.Parse("462cdacf-1d2e-49d7-af10-aefad2d29459") },
                    new SubCategory { Id = Guid.Parse("b2a7c6d8-3f44-4c9a-9f1a-222222222222"), Name = "Sprzedaż", CategoryId = Guid.Parse("462cdacf-1d2e-49d7-af10-aefad2d29459") },
                    new SubCategory { Id = Guid.Parse("b3a7c6d8-3f44-4c9a-9f1a-333333333333"), Name = "Kontrahent", CategoryId = Guid.Parse("462cdacf-1d2e-49d7-af10-aefad2d29459") },
                    new SubCategory { Id = Guid.Parse("b4a7c6d8-3f44-4c9a-9f1a-444444444444"), Name = "Dział IT", CategoryId = Guid.Parse("462cdacf-1d2e-49d7-af10-aefad2d29459") },

                    new SubCategory { Id = Guid.Parse("c1a7c6d8-3f44-4c9a-9f1a-555555555555"), Name = "Rodzina", CategoryId = Guid.Parse("020b977f-e9f0-4310-af8e-1be21fab0a77") },
                    new SubCategory { Id = Guid.Parse("c2a7c6d8-3f44-4c9a-9f1a-666666666666"), Name = "Przyjaciele", CategoryId = Guid.Parse("020b977f-e9f0-4310-af8e-1be21fab0a77") },
                    new SubCategory { Id = Guid.Parse("c3a7c6d8-3f44-4c9a-9f1a-777777777777"), Name = "Znajomi", CategoryId = Guid.Parse("020b977f-e9f0-4310-af8e-1be21fab0a77") },
                    new SubCategory { Id = Guid.Parse("c4a7c6d8-3f44-4c9a-9f1a-888888888888"), Name = "Sąsiedzi", CategoryId = Guid.Parse("020b977f-e9f0-4310-af8e-1be21fab0a77") }
                );
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
