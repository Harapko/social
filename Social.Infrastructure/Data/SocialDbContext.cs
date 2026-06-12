using Microsoft.EntityFrameworkCore;
using Social.Domain.Entities;

namespace Social.Infrastructure.Data;

public sealed class SocialDbContext : DbContext
{
    public SocialDbContext(DbContextOptions<SocialDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedNever();

            entity.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(256);

            entity.HasIndex(x => x.Email)
                .IsUnique();

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.Surname)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(x => x.PasswordHash)
                .IsRequired()
                .HasMaxLength(512);

            entity.Property(x => x.CreatedAt).IsRequired();
            entity.Property(x => x.UpdatedAt).IsRequired();
        });
    }

    public override int SaveChanges()
    {
        ApplyTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyTimestamps()
    {
        var now = DateTimeOffset.UtcNow;

        foreach (var entry in ChangeTracker.Entries<User>())
        {
            if (entry.State == EntityState.Added)
            {
                if (entry.Entity.Id == Guid.Empty)
                    entry.Entity.Id = Guid.NewGuid();

                if (entry.Entity.CreatedAt == default)
                    entry.Entity.CreatedAt = now;

                entry.Entity.UpdatedAt = now;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = now;
                entry.Property(x => x.CreatedAt).IsModified = false;
            }
        }
    }
}

