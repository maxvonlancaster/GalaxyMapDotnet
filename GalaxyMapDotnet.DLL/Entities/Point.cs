using GalaxyMapDotnet.DLL.Utils.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyMapDotnet.DLL.Entities
{
    public class Point
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Name { get; set; }
        public PointCategory Category { get; set; }
        public int Priority { get; set; } // 1, 2, 3
        public string Description { get; set; }

        [NotMapped]
        public double MapX { get; set; }
        [NotMapped]
        public double MapY { get; set; }
    }
}
