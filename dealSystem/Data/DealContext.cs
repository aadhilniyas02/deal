using dealSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace dealSystem.Data;

public class DealContext : DbContext // Db Context works with Database
{
    public DealContext(DbContextOptions<DealContext> options) : base(options) {}

    public DbSet<Deal> DealManagementSystem { get; set; } 
    public DbSet<Hotel> HotelsTable { get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hotel>()
        .HasOne(h => h.Deal)
        .WithMany(d => d.Hotels)
        .HasForeignKey(h => h.DealId)
        .OnDelete(DeleteBehavior.Cascade);
    }

}