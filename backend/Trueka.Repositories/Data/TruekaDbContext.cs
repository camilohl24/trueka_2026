using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Trueka.Entities;
using Trueka.Entities.Common;
namespace Trueka.Repositories.Data;

public class TruekaDbContext : IdentityDbContext<AppUser>
{
    public TruekaDbContext(DbContextOptions<TruekaDbContext> options) : base(options)
    {
    }
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<Offer> Offers => Set<Offer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .UseCollation("SQL_Latin1_General_CP1_CI_AI");

        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();

        modelBuilder.Entity<Item>()
            .HasOne(i => i.Category)
            .WithMany(c => c.Items)
            .HasForeignKey(i => i.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Item>()
            .HasOne(i => i.Owner)
            .WithMany(u => u.Items)
            .HasForeignKey(i => i.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<Offer>()
            .HasOne(o => o.Item)
            .WithMany(i => i.Offers)
            .HasForeignKey(o => o.ItemId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Offer>()
            .HasOne(o => o.OfferedBy)
            .WithMany(u => u.Offers)
            .HasForeignKey(o => o.OfferedById)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var utcNow = DateTime.UtcNow;
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = utcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedDate = utcNow;
                    entry.Property(e => e.CreatedDate).IsModified = false;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
