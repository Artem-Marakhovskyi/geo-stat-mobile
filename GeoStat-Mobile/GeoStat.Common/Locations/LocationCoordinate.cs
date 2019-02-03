using System;
namespace GeoStat.Common.Locations
{
    public class LocationCoordinate
    {
        public DateTimeOffset DateTime { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public LocationCoordinate(
            double longitude,
            double latitude)
        {
            Latitude = latitude;
            Longitude = longitude;
            DateTime = DateTimeOffset.UtcNow;
        }

        public LocationCoordinate()
        {

        }

        public static LocationCoordinate From(string s)
        {
            var tokens = s.Split(',');

            var coord = new LocationCoordinate()
            {
                DateTime = DateTimeOffset.Parse(tokens[0]),
                Longitude = double.Parse(tokens[1]),
                Latitude = double.Parse(tokens[2])
            };

            return coord;
        }
    }
}
