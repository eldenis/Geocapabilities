using Geofencing;

namespace Console.Tests
{
    internal class Program
    {
        static void Main()
        {
            var florida = new PolygonFence.GeoPoint(19.3806445, -99.1791765);

            var poly = new[] { new PolygonFence.GeoPoint(19.3827, -99.17646), new PolygonFence.GeoPoint(19.38181, -99.18007), new PolygonFence.GeoPoint(19.38415, -99.1841), new PolygonFence.GeoPoint(19.3759, -99.1877), new PolygonFence.GeoPoint(19.37363, -99.17921), new PolygonFence.GeoPoint(19.38132, -99.17642), new PolygonFence.GeoPoint(19.3831, -99.17599) };
            var polySinFlorida = new[] { new PolygonFence.GeoPoint(19.3827, -99.17646), new PolygonFence.GeoPoint(19.37881, -99.17822), new PolygonFence.GeoPoint(19.38415, -99.1841), new PolygonFence.GeoPoint(19.3759, -99.1877), new PolygonFence.GeoPoint(19.37363, -99.17921), new PolygonFence.GeoPoint(19.38132, -99.17642), new PolygonFence.GeoPoint(19.3831, -99.17599) };

            PolygonFence.IsPointInPolygon(poly, florida).Dump("Este polígono contiene a Florida 36, debe ser True");
            PolygonFence.IsPointInPolygon(polySinFlorida, florida).Dump("Este polígono NO contiene a Florida 36, debe ser False");
        }
    }

    public static class Ex
    {
        public static void Dump<T>(this T obj, string msg) where T : new()
        {
            System.Console.WriteLine($"{msg} : {obj}");
        }
    }
}
