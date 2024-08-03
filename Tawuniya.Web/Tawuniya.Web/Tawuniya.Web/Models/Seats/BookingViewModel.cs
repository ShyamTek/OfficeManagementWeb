namespace Tawuniya.Web.Models
{
    public class BookingViewModel
    {
        public int LayoutId { get; set; }
        public float SeatX { get; set; }
        public float SeatY { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
