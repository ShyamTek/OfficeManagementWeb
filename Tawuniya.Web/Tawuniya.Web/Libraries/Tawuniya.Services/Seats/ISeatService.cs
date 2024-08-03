using Tawuniya.Core.Domain.Seats;

namespace Tawuniya.Services.Seats
{
    public interface ISeatService
    {

        Task<IList<Polygon>> GetSeatsAsync();

        Task InsertSeatAsync(Polygon seat);

        Task<Polygon> GetSeatByIdAsync(int id);

        Task<Polygon> GetSeatByLocationAsync(float seatX, float seatY);

        Task<IList<Polygon>> GetSeatByLayoutIdAsync(int layoutId);

        Task InsertBookingAsync(Booking booking);
    }
}
