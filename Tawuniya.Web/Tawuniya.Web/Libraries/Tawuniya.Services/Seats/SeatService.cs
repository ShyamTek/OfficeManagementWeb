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

        public async Task<IList<Seat>> GetSeatsAsync()
        {
            return await _context.Seat.ToListAsync();

        }
        public async Task InsertSeatAsync(Seat seat)
        {
            if (seat == null)
                throw new ArgumentNullException(nameof(seat));

            await _context.Seat.AddAsync(seat);
            await _context.SaveChangesAsync();
        }

        public async Task<Seat> GetSeatByIdAsync(int id)
        {
            return await _context.Seat.FindAsync(id);
        }
        
        public async Task<Seat> GetSeatByLocationAsync(float seatX, float seatY)
        {
            return  _context.Seat.Where(x => x.X == seatX && x.Y == seatY).FirstOrDefault();
        }
        
        public async Task<IList<Seat>> GetSeatByLayoutIdAsync(int layoutId)
        {
            return  _context.Seat.Where(s=>s.LayoutId==s.LayoutId).ToList();
        }

        public async Task InsertBookingAsync(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(nameof(booking));

            await _context.Booking.AddAsync(booking);
            await _context.SaveChangesAsync();
        }


        #endregion
    }
}
