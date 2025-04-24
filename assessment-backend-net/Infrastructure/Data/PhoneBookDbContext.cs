using Microsoft.EntityFrameworkCore;
using PhoneBook.Domain.Entities;

namespace PhoneBook.Infrastructure.Data;

public class PhoneBookDbContext : DbContext
{
    public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options)
        : base(options)
    {
    }

    public DbSet<Person> Persons => Set<Person>();
    public DbSet<ContactInfo> ContactInfos => Set<ContactInfo>();
    public DbSet<Report> Reports => Set<Report>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>()
            .HasMany(p => p.ContactInfos)
            .WithOne(c => c.Person)
            .HasForeignKey(c => c.PersonId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ContactInfo>()
            .Property(c => c.Type)
            .HasConversion<string>();

        modelBuilder.Entity<Report>()
            .Property(r => r.Status)
            .HasConversion<string>();
    }
}
