using GalaxyMapDotnet.DLL.Entities;
using GalaxyMapDotnet.DLL.Serivces.Interfaces;
using GalaxyMapDotnet.DLL.Utils.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;

namespace GalaxyMapDotnet.DLL.Serivces
{
    public class MapService : IMapService
    {
        private MapContext _context;

        public MapService(MapContext context)
        {
            _context = context;
        }

        public PointMap GetSubMap(MapRequest request)
        {
            var result = new PointMap();

            var doubleCoordinates = MapToAbsolute(request, request.Width / 2 + (request.MapX - request.Width / 2) * 0.2,
                request.Height / 2 + (request.MapY - request.Height / 2) * 0.2);
            result.X = Convert.ToInt32(doubleCoordinates.Item1);
            result.Y = Convert.ToInt32(doubleCoordinates.Item2);

            var sw = (result.X - 50000 * Math.Pow(0.8, request.Zoom - 1),
                result.Y - 50000 * Math.Pow(0.8, request.Zoom - 1));
            //var nw = (result.X - 50000 * Math.Pow(0.8, request.Zoom - 1), result.Y + 50000 * Math.Pow(0.8, request.Zoom - 1));
            var ne = (result.X + 50000 * Math.Pow(0.8, request.Zoom - 1),
                result.Y + 50000 * Math.Pow(0.8, request.Zoom - 1));
            //var se = (result.X + 50000 * Math.Pow(0.8, request.Zoom - 1), result.Y - 50000 * Math.Pow(0.8, request.Zoom - 1));

            result.Points = _context.Points.Where(p => (p.X > sw.Item1) && (p.X < ne.Item1) &&
                (p.Y > sw.Item2) && (p.Y < ne.Item2) && GetPriorities(request.Zoom).Contains(p.Priority));

            foreach (var point in result.Points)
            {
                (point.MapX, point.MapY) = AbsoluteToMap(request, point.X, point.Y);
            }

            result.Zoom = request.Zoom;

            var regions = _context.Regions.ToList();
            //var transformedRegions = new List<Region>();
            foreach (var region in regions)
            {
                var points = region.CounturPoints.Split('|');
                var transformedPoints = "|";
                foreach (var point in points)
                {
                    if (!point.IsNullOrEmpty())
                    {
                        (var x, var y) = AbsoluteToMap(request,
                            Convert.ToDouble(point.Split(',')[0]),
                            Convert.ToDouble(point.Split(',')[1]));
                        transformedPoints += x.ToString() + "," + y.ToString() + "|";
                    }
                }
                region.CounturPoints = transformedPoints;
            }

            result.Regions = regions;



            result.Lines = new List<Line>(){new Line(){},new Line(){}};

            (result.Lines[0].XStart, result.Lines[0].YStart) 
                = AbsoluteToMap(request, -50000, 27000);
            (result.Lines[0].XEnd, result.Lines[0].YEnd)
                = AbsoluteToMap(request, 50000, 27000);

            (result.Lines[1].XStart, result.Lines[1].YStart)
                = AbsoluteToMap(request, 0, -23000);
            (result.Lines[1].XEnd, result.Lines[1].YEnd)
                = AbsoluteToMap(request, 0, 77000);

            result.Circles = new List<Circle>(){new Circle(){}};
            (result.Circles[0].X, result.Circles[0].Y)
                = AbsoluteToMap(request, 0, 27000);
            result.Circles[0].R = 1 * request.Width / (2 * Math.Pow(0.8, request.Zoom - 1));

            return result;
        }

        private (double, double) MapToAbsolute(
            MapRequest request,
            double valueX,
            double valueY) 
        {
            return (request.X + (valueX - request.Width / 2) / request.Width * (100000* Math.Pow( 0.8, request.Zoom - 1)),
                request.Y + (valueY - request.Height / 2) / request.Height * (100000 * Math.Pow(0.8, request.Zoom - 1)));
        }

        private (double, double) AbsoluteToMap(
            MapRequest request,
            double valueX,
            double valueY)
        {
            return ((valueX - request.X) * request.Width / (100000 * Math.Pow(0.8, request.Zoom - 1)) + request.Width / 2,
                (valueY - request.Y) * request.Height / (100000 * Math.Pow(0.8, request.Zoom - 1)) + request.Height / 2);
        }

        private List<int> GetPriorities(int zoom) 
        {
            switch (zoom)
            {
                case int n when (n < 10):
                    return new List<int> { 1 };
                    break;
                case int n when (n >= 10 && n <20):
                    return new List<int> { 1, 2 };
                    break;
                default: return new List<int> { 1, 2, 3 };
            }
        }
    }
}
