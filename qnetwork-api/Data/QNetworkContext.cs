using Microsoft.EntityFrameworkCore;
using qnetwork_api.Models;

namespace qnetwork_api.Data
{
    public class QNetworkContext : DbContext
    {
        public QNetworkContext(DbContextOptions<QNetworkContext> options) : base(options) { }

        public DbSet<IndustrialDevice> IndustrialDevices { get; set; } = null!;
        public DbSet<Network> Networks { get; set; } = null!;
        public DbSet<IndustrialDeviceNetworkMapping> IndustrialDeviceNetworkMappings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many mapping as explicit entity
            modelBuilder.Entity<IndustrialDeviceNetworkMapping>()
                .HasIndex(d => new { d.IndustrialDeviceID, d.NetworkID })
                .IsUnique();

            // Cascade deletes: when device removed, delete mappings
            modelBuilder.Entity<IndustrialDeviceNetworkMapping>()
                .HasOne(d => d.IndustrialDevice)
                .WithMany(d => d.NetworkMappings)
                .HasForeignKey(d => d.IndustrialDeviceID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IndustrialDeviceNetworkMapping>()
                .HasOne(d => d.Network)
                .WithMany(n => n.IndustrialDeviceMappings)
                .HasForeignKey(d => d.NetworkID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
