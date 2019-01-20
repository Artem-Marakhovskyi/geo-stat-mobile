using System;
using GeoStat.Common.Abstractions;

namespace GeoStat.Common.Models
{
    public class GeoStatUser : TableData
    {
        public string Email { get; set; }

        public string UserId { get; set; }
    }
}
