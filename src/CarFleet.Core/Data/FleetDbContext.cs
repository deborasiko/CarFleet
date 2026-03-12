using Microsoft.EntityFrameworkCore;
using CarFleet.Core.Models;

namespace CarFleet.Core.Data;

public class FleetDbContext : DbContext
{
    public FleetDbContext(DbContextOptions<FleetDbContext> options) : base(options) { }

    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Driver> Drivers => Set<Driver>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<VehicleAssignment> VehicleAssignments => Set<VehicleAssignment>();
    public DbSet<FuelLog> FuelLogs => Set<FuelLog>();
    public DbSet<ServiceRecord> ServiceRecords => Set<ServiceRecord>();
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<FleetLocation> FleetLocations => Set<FleetLocation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Vehicle relationships
        modelBuilder.Entity<Vehicle>()
            .HasOne(v => v.FleetLocation)
            .WithMany(fl => fl.Vehicles)
            .HasForeignKey(v => v.FleetLocationId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Vehicle>()
            .HasMany(v => v.VehicleAssignments)
            .WithOne(va => va.Vehicle)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Vehicle>()
            .HasMany(v => v.FuelLogs)
            .WithOne(fl => fl.Vehicle)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Vehicle>()
            .HasMany(v => v.ServiceRecords)
            .WithOne(sr => sr.Vehicle)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Vehicle>()
            .HasMany(v => v.Trips)
            .WithOne(t => t.Vehicle)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Vehicle>()
            .HasMany(v => v.Expenses)
            .WithOne(e => e.Vehicle)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Vehicle>()
            .HasMany(v => v.Documents)
            .WithOne(d => d.Vehicle)
            .OnDelete(DeleteBehavior.Cascade);

        // Driver relationships
        modelBuilder.Entity<Driver>()
            .HasMany(d => d.VehicleAssignments)
            .WithOne(va => va.Driver)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Driver>()
            .HasMany(d => d.Trips)
            .WithOne(t => t.Driver)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Driver>()
            .HasMany(d => d.FuelLogs)
            .WithOne(fl => fl.Driver)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Driver>()
            .HasOne(d => d.User)
            .WithOne(u => u.Driver)
            .HasForeignKey<User>(u => u.DriverId)
            .OnDelete(DeleteBehavior.SetNull);

        // VehicleAssignment indexes
        modelBuilder.Entity<VehicleAssignment>()
            .HasKey(va => va.Id);

        modelBuilder.Entity<VehicleAssignment>()
            .HasIndex(va => new { va.VehicleId, va.DriverId });

        // User & Role relationships
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // FuelLog foreign keys nullable
        modelBuilder.Entity<FuelLog>()
            .HasOne(fl => fl.Driver)
            .WithMany(d => d.FuelLogs)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        // Trip foreign keys nullable
        modelBuilder.Entity<Trip>()
            .HasOne(t => t.Driver)
            .WithMany(d => d.Trips)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        base.OnModelCreating(modelBuilder);
    }
}