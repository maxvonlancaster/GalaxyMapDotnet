using GalaxyMapDotnet.DLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyMapDotnet.DLL.Utils.Dtos
{
    public class PointMap
    {
        public int Zoom { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public IEnumerable<Point> Points { get; set; }
        public IEnumerable<Region> Regions { get; set; }
        public List<Line> Lines { get; set; }
        public List<Circle> Circles { get; set; }
    }
}
