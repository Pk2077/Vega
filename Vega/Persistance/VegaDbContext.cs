using Microsoft.EntityFrameworkCore;
using Vega.Models;

namespace Vega.Persistance
{
    public class VegaDbContext : DbContext
    {
        public DbSet<Model> Models { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public VegaDbContext()
        {

        }
        public VegaDbContext(DbContextOptions<VegaDbContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                 .HasMany(v => v.Photos)
                 .WithOne(p => p.Vehicle)
                 .HasForeignKey(p => p.VehicleId)
                 .OnDelete(DeleteBehavior.Cascade);
/**/
            modelBuilder.Entity<VehicleFeature>().HasKey(vf =>
              new { vf.VehicleId, vf.FeatureId });

            base.OnModelCreating(modelBuilder);
        }

    }
}
