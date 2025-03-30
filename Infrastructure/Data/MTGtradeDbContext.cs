using Domain.Entites;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class MTGtradeDbContext : DbContext
{
    public DbSet<Set> Sets { get; set; }
    public DbSet<Card> Cards { get; set; }

    public MTGtradeDbContext(DbContextOptions<MTGtradeDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 🔒 Lägg till unik constraint på Name + Number + SetId
        modelBuilder.Entity<Card>()
            .HasIndex(c => new { c.Name, c.Number, c.SetId, c.Layout })
            .IsUnique();
    }
}
