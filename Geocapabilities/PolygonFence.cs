using System;
using System.Collections.Generic;

namespace Geocapabilities
{
    public partial class PolygonFence
    {
        public static bool IsPointInPolygon(IList<GeoPoint> poly, GeoPoint point)
        {
            bool PointTest(int i, int j)
            {
                return (poly[i].Latitude <= point.Latitude && (point.Latitude < poly[j].Latitude) || poly[j].Latitude <= point.Latitude && point.Latitude < poly[i].Latitude) &&
                       point.Longitude < (poly[j].Longitude - poly[i].Longitude) * (point.Latitude - poly[i].Latitude) / (poly[j].Latitude - poly[i].Latitude) + poly[i].Longitude;

            }

            var c = false;
            for (var t = (i: 0, j: poly.Count - 1); t.i < poly.Count; t.j = t.i++)
            {
                if (PointTest(t.i, t.j)) c = !c;
            }

            return c;
        }
    }
}
