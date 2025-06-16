using Microsoft.EntityFrameworkCore;
using ToTienDung0300567.Entities;

namespace ToTienDung0300567.DbContexts;

public class AppDbContexts0300567De21026 : DbContext
{
    public DbSet<Mechanic0300567De20945> Mechanics { get; set; }
    public DbSet<Vehicle0300567De20949> Vehicles { get; set; }
    public DbSet<RepairRecord0300567De20952> RepairRecords { get; set; }

    public AppDbContexts0300567De21026(DbContextOptions<AppDbContexts0300567De21026> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Unique constraints
        modelBuilder.Entity<Mechanic0300567De20945>()
            .HasIndex(m => m.maTho)
            .IsUnique();
            
        modelBuilder.Entity<Mechanic0300567De20945>()
            .HasIndex(m => m.tenTho)
            .IsUnique();
            
        modelBuilder.Entity<Mechanic0300567De20945>()
            .HasIndex(m => m.cCCD)
            .IsUnique();
            
        modelBuilder.Entity<Vehicle0300567De20949>()
            .HasIndex(v => v.bienSoXe)
            .IsUnique();

        // Relationships
        modelBuilder.Entity<RepairRecord0300567De20952>()
            .HasOne(r => r.mechanic)
            .WithMany(m => m.RepairRecords)
            .HasForeignKey(r => r.mechanicId);

        modelBuilder.Entity<RepairRecord0300567De20952>()
            .HasOne(r => r.vehicle)
            .WithMany(v => v.RepairRecords)
            .HasForeignKey(r => r.vehicleId);
    }
}