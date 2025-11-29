using Microsoft.EntityFrameworkCore;
using Ordering.API.Entities;

namespace Ordering.API.Data;

public class OrderContext : DbContext
{
    public OrderContext(DbContextOptions<OrderContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasConversion<string>();

        modelBuilder.Entity<OutboxMessage>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.CorrelationId); //for faster lookups
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.OccurredOn).IsRequired();
            builder.Property(x => x.ProcessedOn).IsRequired(false);
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.CreatedBy = "dav";
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = "dh";
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
