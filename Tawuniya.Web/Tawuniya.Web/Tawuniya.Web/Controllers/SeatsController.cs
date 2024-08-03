using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tawuniya.Core.Domain.Seats;
using Tawuniya.Data;
using Tawuniya.Services.Layouts;
using Tawuniya.Services.Seats;
using Tawuniya.Web.Models;
using Tawuniya.Web.Models.Seats;

namespace Tawuniya.Web.Controllers
{
    public class SeatsController : Controller
    {
        private readonly ISeatService _seatService;
        private readonly ILayoutService _layoutService;
        private readonly TawuniyaDBContext _context;

        public SeatsController(ISeatService seatService,
            ILayoutService layoutService,
            TawuniyaDBContext context)
        {
            _seatService = seatService;
            _layoutService = layoutService;
            _context = context;
        }
        #region today3

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var layouts = await _layoutService.GetLayoutsAsync();
            //var layoutViewModel = layouts.Select(layout =>
            //     new Models.LayoutViewModel
            //     {
            //         Id = layout.Id,
            //         Name = layout.Name,
            //         ImageUrl = layout.ImageUrl
            //     }).ToList();
            //return View(layoutViewModel);
            var layouts = await _context.Layouts.ToListAsync();
            var layoutViewModel = layouts.Select(layout =>
                 new LayoutViewModel
                 {
                     Id = layout.Id,
                     Name = layout.Name,
                     ImageUrl = layout.ImageUrl
                 }).ToList();
            return View(layoutViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> MarkSeat(int layoutId)
        {
            var layout = await _context.Layouts.FindAsync(layoutId);
            if (layout == null)
            {
                return NotFound();
            }
            ViewData["LayoutId"] = layoutId;
            var layoutModel = new LayoutViewModel()
            {
                Id = layout.Id,
                Name = layout.Name,
                ImageUrl = layout.ImageUrl
            };
            return View(layoutModel);
        }

        [HttpPost]
        public async Task<IActionResult> MarkSeat([FromBody] PolygonViewModel model)
        {
            var polygon = new Polygon
            {
                LayoutId = model.LayoutId,
                Category = model.Category,
                Coordinates = model.Coordinates.Select(c => new PolygonCoordinate { X = c.X, Y = c.Y }).ToList()
            };

            _context.Polygons.Add(polygon);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> GetLayoutPolygons(int layoutId)
        {
            var polygons = await _context.Polygons
            .Where(p => p.LayoutId == layoutId)
            .Include(p => p.Coordinates)
            .ToListAsync();

            var polygonViewModels = polygons.Select(p => new PolygonViewModel
            {
                Id = p.Id,
                LayoutId = p.LayoutId,
                Category = p.Category,
                Coordinates = p.Coordinates.Select(c => new CoordinateViewModel { X = c.X, Y = c.Y }).ToList()
            }).ToList();

            return Json(polygonViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> BookSeat()
        {
            var layouts = await _context.Layouts.ToListAsync();
            var layoutViewModel = layouts.Select(layout =>
                 new LayoutViewModel
                 {
                     Id = layout.Id,
                     Name = layout.Name,
                     ImageUrl = layout.ImageUrl
                 }).ToList();
            return View(layoutViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> BookSeat([FromBody] BookingViewModel model)
        {
            var userId = "1";
            if (userId == null)
            {
                return Unauthorized();
            }


            var polygon = await _context.Polygons
                .Include(p => p.Coordinates)
                .FirstOrDefaultAsync(p => p.Id == model.PolygonId);
            var booking = new Booking
            {
                PolygonId = polygon.Id,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                UserId = userId
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        #endregion
    }
}
