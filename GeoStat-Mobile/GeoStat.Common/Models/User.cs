using System;
using GeoStat_Mobile.Abstractions;

namespace GeoStat_Mobile.Models
{
    public class User : TableData
    {
        public string Email { get; set; }

        public string UserId { get; set; }
    }
}
