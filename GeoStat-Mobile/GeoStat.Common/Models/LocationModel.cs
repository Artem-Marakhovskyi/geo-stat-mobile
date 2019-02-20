using System;
using GeoStat.Common.Services;

namespace GeoStat.Common.Models
{
    public class LocationModel
    {
        public double Latitude { get; private set; }

        public double Longitude { get; private set; }

        public DateTimeOffset DateTime { get; private set; }

        public string UserId { get; private set; }

        //necessary for mapper
        public LocationModel() { }

        public LocationModel(double latitude, double longitude, DateTimeOffset date)
        {
            Latitude = latitude;
            Longitude = longitude;
            DateTime = date;
        }

        public LocationModel(double latitude, double longitude, DateTimeOffset date, string id)
                                : this(latitude, longitude, date)
        {
            UserId = id;
        }
    }
}
