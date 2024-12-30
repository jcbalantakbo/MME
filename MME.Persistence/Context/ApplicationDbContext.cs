using Microsoft.EntityFrameworkCore;
using MME.Persistence.Entities;

namespace MME.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Loan> Loans { get; set; } = null!;
        public DbSet<Person> Persons { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<MobileBlacklist> MobileBlacklist { get; set; } = null!;
        public DbSet<EmailDomainBlacklist> EmailDomainBlacklist { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Loan Entity Configuration
            modelBuilder.Entity<Loan>(entity =>
            {
                entity.Property(e => e.Id)
                      .HasMaxLength(100);

                // Specify precision and scale for decimal properties
                entity.Property(e => e.AmountRequired)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();
                entity.Property(e => e.Term).IsRequired();
                entity.Property(e => e.RepaymentAmount)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.HasOne(e => e.Person)
                      .WithMany(p => p.Loans)
                      .HasForeignKey(e => e.PersonId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.Product)
                      .WithMany()
                      .HasForeignKey(p => p.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Person Entity Configuration
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title)
                      .HasMaxLength(10)
                      .IsRequired();
                entity.Property(e => e.FirstName)
                      .HasMaxLength(50)
                      .IsRequired();
                entity.Property(e => e.LastName)
                      .HasMaxLength(50)
                      .IsRequired();
                entity.Property(e => e.DateOfBirth).IsRequired();
                entity.Property(e => e.Mobile)
                      .HasMaxLength(15)
                      .IsRequired();
                entity.Property(e => e.Email)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.HasIndex(e => e.Email).IsUnique();
            });


            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.InterestRate)
                     .HasColumnType("decimal(18,2)")
                     .IsRequired();
            });

            // Seed Data for Product
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "ProductA", InterestRate = 0, MinimumTerm = 6, IsInterestFree = true },
                new Product { Id = 2, Name = "ProductB", InterestRate = 5, MinimumTerm = 6, IsInterestFree = false },
                new Product { Id = 3, Name = "ProductC", InterestRate = 10, MinimumTerm = 6, IsInterestFree = false }
            );

            // MobileBlacklist Entity Configuration
            modelBuilder.Entity<MobileBlacklist>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MobileNumber)
                      .HasMaxLength(15)
                      .IsRequired();
                entity.HasIndex(e => e.MobileNumber).IsUnique();
            });

            // Seed Data for MobileBlacklist
            modelBuilder.Entity<MobileBlacklist>().HasData(
                new MobileBlacklist { Id = 1, MobileNumber = "1234567890" },
                new MobileBlacklist { Id = 2, MobileNumber = "9876543210" },
                new MobileBlacklist { Id = 3, MobileNumber = "5555555555" }
            );

            // EmailDomainBlacklist Entity Configuration
            modelBuilder.Entity<EmailDomainBlacklist>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Domain)
                      .HasMaxLength(255)
                      .IsRequired();
                entity.HasIndex(e => e.Domain).IsUnique();
            });

            // Seed Data for EmailDomainBlacklist
            modelBuilder.Entity<EmailDomainBlacklist>().HasData(
                new EmailDomainBlacklist { Id = 1, Domain = "blacklistdomain.com" },
                new EmailDomainBlacklist { Id = 2, Domain = "spamdomain.com" },
                new EmailDomainBlacklist { Id = 3, Domain = "bannedemail.com" }
            );
        }
    }
}
