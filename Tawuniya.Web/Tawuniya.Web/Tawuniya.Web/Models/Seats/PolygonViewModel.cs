namespace Tawuniya.Web.Models.Seats
{
    public class PolygonViewModel
    {
        public int Id { get; set; }
        public int LayoutId { get; set; }
        public string Category { get; set; }
        public List<CoordinateViewModel> Coordinates { get; set; }
    }
}
