using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyMapDotnet.DLL.Utils.Dtos
{
    public class MapRequest
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public double MapX { get; set; }
        public double MapY { get; set; }
        public int Zoom { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
