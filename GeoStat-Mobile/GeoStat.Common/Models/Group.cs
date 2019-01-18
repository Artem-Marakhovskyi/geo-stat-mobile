using System;
using GeoStat.Common.Abstractions;

namespace GeoStat.Common.Models
{
    public class Group : TableData
    {
        public string Label { get; set; }

        public string CreatorId { get; set; }
    }
}
