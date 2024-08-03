using Microsoft.EntityFrameworkCore;
using Tawuniya.Core.Domain.Seats;
using Tawuniya.Data;

namespace Tawuniya.Services.Seats
{
    public class SeatService : ISeatService
    {
        #region Fields

        private readonly TawuniyaDBContext _context;

        #endregion

        #region Ctor

        public SeatService(TawuniyaDBContext context)
        {
            _context = context;
        }

        #endregion


        #region Methods

        public async Task<IList<Polygon>> GetSeatsAsync()
        {
            return await _context.Polygons.ToListAsync();

        }
        public async Task InsertSeatAsync(Polygon seat)
        {
            if (seat == null)
                throw new ArgumentNullException(nameof(seat));

            await _context.Polygons.AddAsync(seat);
            await _context.SaveChangesAsync();
        }

        public async Task<Polygon> GetSeatByIdAsync(int id)
        {
            return await _context.Polygons.FindAsync(id);
        }
        
        //public async Task<Polygon> GetSeatByLocationAsync(float seatX, float seatY)
        //{
        //    return  _context.Polygons.Where(x => x.X == seatX && x.Y == seatY).FirstOrDefault();
        //}
        
        public async Task<IList<Polygon>> GetSeatByLayoutIdAsync(int layoutId)
        {
            return  _context.Polygons.Where(s=>s.LayoutId==s.LayoutId).ToList();
        }

        public async Task InsertBookingAsync(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(nameof(booking));

            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public Task<Polygon> GetSeatByLocationAsync(float seatX, float seatY)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
