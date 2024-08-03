using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tawuniya.Core.Domain.Seats
{
    public class PolygonCoordinate : BaseEntity
    {
        public int PolygonId { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public Polygon Polygon { get; set; }
    }
}
