using GalaxyMapDotnet.DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapDotnet.DLL
{
    public class MapContext : DbContext
    {
        public MapContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Point> Points { get; set; }
        public DbSet<Region> Regions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Point>().ToTable("Point");
            modelBuilder.Entity<Region>().ToTable("Region");
        }
    }
}