using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tawuniya.Core.Domain.Layouts;

namespace Tawuniya.Core.Domain.Seats
{
    public class Polygon : BaseEntity
    {
        public int LayoutId { get; set; }
        public Layout Layout { get; set; }
        public List<PolygonCoordinate> Coordinates { get; set; } = new List<PolygonCoordinate>();
        public string Category { get; set; }
    }
}
