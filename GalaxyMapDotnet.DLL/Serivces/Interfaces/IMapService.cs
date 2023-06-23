using GalaxyMapDotnet.DLL.Utils.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyMapDotnet.DLL.Serivces.Interfaces
{
    public interface IMapService
    {
        PointMap GetSubMap(MapRequest request);
    }
}
