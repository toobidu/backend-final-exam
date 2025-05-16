using Demo.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demo.DbContexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Store> Stores { get; set; } = null!;
    public DbSet<Supplier> Suppliers { get; set; } = null!;
    public DbSet<StoreSupplier> StoreSuppliers { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Store entity
        modelBuilder.Entity<Store>()
            .HasIndex(s => s.storeName)
            .IsUnique();
            
        // Configure Supplier entity
        modelBuilder.Entity<Supplier>()
            .HasIndex(s => s.supplierName)
            .IsUnique();
            
        // Configure many-to-many relationship
        modelBuilder.Entity<StoreSupplier>()
            .HasKey(ss => new { ss.StoreId, ss.SupplierId });
            
        modelBuilder.Entity<StoreSupplier>()
            .HasOne(ss => ss.Store)
            .WithMany(s => s.storeSuppeliers)
            .HasForeignKey(ss => ss.StoreId);
            
        modelBuilder.Entity<StoreSupplier>()
            .HasOne(ss => ss.Supplier)
            .WithMany(s => s.storeSuppliers)
            .HasForeignKey(ss => ss.SupplierId);
    }
}