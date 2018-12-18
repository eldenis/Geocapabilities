namespace Geocapabilities
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
