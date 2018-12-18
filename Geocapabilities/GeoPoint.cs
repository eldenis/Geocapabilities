using Newtonsoft.Json;

namespace Geocapabilities
{

    public struct GeoPoint
    {
        [JsonProperty("lat")]
        public double Latitude { get; }
        [JsonProperty("lng")]
        public double Longitude { get; }

        public GeoPoint(double lat, double lng)
        {
            Latitude = lat;
            Longitude = lng;
        }
    }
}
