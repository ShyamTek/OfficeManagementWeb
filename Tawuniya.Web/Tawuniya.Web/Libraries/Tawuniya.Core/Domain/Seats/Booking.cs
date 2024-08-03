using Tawuniya.Core.Domain.Layouts;

namespace Tawuniya.Core.Domain.Seats
{
    public class Booking : BaseEntity
    {
        public int PolygonId { get; set; }
        public Polygon Polygon { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string UserId { get; set; }
        public string EmployeeId { get; set; }
    }

}
