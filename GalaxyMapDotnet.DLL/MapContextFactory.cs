using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyMapDotnet.DLL
{
    public class MapContextFactory : IDesignTimeDbContextFactory<MapContext>
    {
        public MapContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MapContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GalaxyMap;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new MapContext(optionsBuilder.Options);
        }
    }
}
