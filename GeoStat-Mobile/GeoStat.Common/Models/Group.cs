using System;
using GeoStat_Mobile.Abstractions;

namespace GeoStat_Mobile.Models
{
    public class Group : TableData
    {
        public string Label { get; set; }

        public string CreatorId { get; set; }
    }
}
