using System;
using GeoStat_Mobile.Abstractions;

namespace GeoStat_Mobile.Models
{
    public class GroupUser : TableData
    {
        public string GroupId { get; set; }

        public string UserId { get; set; }
    }
}
