using GalaxyMapDotnet.DLL.Serivces.Interfaces;
using GalaxyMapDotnet.DLL.Utils.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GalaxyMapDotnet.Controllers
{
    public class MapController : Controller
    {
        private readonly IMapService _mapService;

        public MapController(IMapService mapService)
        {
            _mapService = mapService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetMap([FromBody] MapRequest model) 
        {
            var result = _mapService.GetSubMap(model);
            return new JsonResult(result);
        }
    }
}
