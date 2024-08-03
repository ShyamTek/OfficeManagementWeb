using Tawuniya.Core.Domain.Seats;

namespace Tawuniya.Services.Seats
{
    public interface ISeatService
    {

        Task<IList<Seat>> GetSeatsAsync();

        Task InsertSeatAsync(Seat seat);

        Task<Seat> GetSeatByIdAsync(int id);

        Task<Seat> GetSeatByLocationAsync(float seatX, float seatY);

        Task<IList<Seat>> GetSeatByLayoutIdAsync(int layoutId);

        Task InsertBookingAsync(Booking booking);
    }
}
