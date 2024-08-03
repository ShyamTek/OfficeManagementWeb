namespace Tawuniya.Core.Domain.Seats
{
    public class BookingPolygonCoordinates : BaseEntity
    {
        public int PolygonId { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public Booking Booking { get; set; }
    }
}
