using System;
using GeoStat.Common.Abstractions;

namespace GeoStat.Common.Models
{
    public class GroupUser : TableData
    {
        public string GroupId { get; set; }

        public string UserId { get; set; }
    }
}
