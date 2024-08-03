namespace Tawuniya.Core.Domain.Seats
{
    public class Seat : BaseEntity
    {
        public int LayoutId { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }
}
