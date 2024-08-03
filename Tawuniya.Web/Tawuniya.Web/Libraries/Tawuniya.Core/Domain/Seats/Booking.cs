namespace Tawuniya.Core.Domain.Seats
{
    public class Booking : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int LayoutId { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }
}
