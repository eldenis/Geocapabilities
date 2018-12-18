namespace Geofencing
{
    public partial class PolygonFence
    {
        public struct GeoPoint
        {
            public double Latitude { get; }
            public double Longitude { get; }

            public GeoPoint(double lt, double lg)
            {
                Latitude = lt;
                Longitude = lg;
            }
        }
    }
}
