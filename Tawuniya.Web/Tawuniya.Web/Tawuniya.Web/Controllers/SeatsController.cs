using Microsoft.AspNetCore.Mvc;
using Tawuniya.Core.Domain.Seats;
using Tawuniya.Services.Layouts;
using Tawuniya.Services.Seats;
using Tawuniya.Web.Models;

namespace Tawuniya.Web.Controllers
{
    public class SeatsController : Controller
    {
        private readonly ISeatService _seatService;
        private readonly ILayoutService _layoutService;

        public SeatsController(ISeatService seatService,
            ILayoutService layoutService)
        {
            _seatService = seatService;
            _layoutService = layoutService;
        }

        // GET: Seats/BookSeats
        [HttpGet]
        public async Task<IActionResult> BookSeats()
        {
            var layouts= await _layoutService.GetLayoutsAsync();
            var layoutViewModel = layouts.Select( layout =>
                new LayoutViewModel
                {
                    Id = layout.Id,
                    Name = layout.Name,
                }).ToList();    
            return View(layouts);
        }

        [HttpPost]
        public async Task<IActionResult> BookSeat([FromForm] BookingViewModel model)
        {
            //var userId = 1;
            //if (userId == null)
            //{
            //    return Unauthorized();
            //}
            var seat = await _seatService.GetSeatByLocationAsync(model.SeatX, model.SeatY);
            var booking = new Booking
            {
                LayoutId = model.LayoutId,
                X = model.SeatX,
                Y = model.SeatY,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                UserId = 1
            };
            await _seatService.InsertBookingAsync(booking);
            return Json(new { success = true });
        }


        [HttpGet]
        public async Task<IActionResult> MarkSeats()
        {
            var layouts = await _layoutService.GetLayoutsAsync();
            var layoutViewModel = layouts.Select(layout =>
                new LayoutViewModel
                {
                    Id = layout.Id,
                    Name = layout.Name,
                }).ToList();
            return View(layoutViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> MarkSeat([FromBody] SeatViewModel model)
        {
            var seat = new Seat
            {
                LayoutId = model.LayoutId,
                X = model.X,
                Y = model.Y
            };
            await _seatService.InsertSeatAsync(seat);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetMarkers(int layoutId)
        {
            try
            {
                var markers =await _seatService.GetSeatsAsync();
                var ss = markers.Select(s => new { s.X, s.Y })
                   .ToList();
                return Json(ss);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
