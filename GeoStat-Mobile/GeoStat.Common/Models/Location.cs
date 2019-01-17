using System;
using GeoStat_Mobile.Abstractions;

namespace GeoStat_Mobile.Models
{
    public class Location : TableData
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTimeOffset DateTime { get; set; }

        public string UserId { get; set; }
    }
}
