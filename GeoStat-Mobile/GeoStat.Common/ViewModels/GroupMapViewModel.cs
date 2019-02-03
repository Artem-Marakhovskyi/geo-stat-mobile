using System;
using System.Collections.Generic;
using System.Text;
using GeoStat.Common.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace GeoStat.Common.ViewModels
{
    public class GroupMapViewModel : MvxViewModel
    {
        //public Group CurrentGroup { get; }

        public IEnumerable<UserModel> GroupMembers { get; }
        
        public IEnumerable<Location> GroupLocations { get;  }

        public GroupMapViewModel()
        {
            // Add test group
            GroupMembers = new List<UserModel>
            {
                new UserModel { UserId = "user1" },
                new UserModel { UserId = "user2" },
                new UserModel { UserId = "user3" },
                new UserModel { UserId = "user4" },
                new UserModel { UserId = "user5" }
            };

            GroupLocations = new List<Location>
            {
                new Location {UserId = "user1", Latitude = 7.3, Longitude = 67.2},
                new Location {UserId = "user2", Latitude = 0.3, Longitude = 0.2},
                new Location {UserId = "user3", Latitude = 12.4, Longitude = 12.5},
                new Location {UserId = "user1", Latitude = 34.7, Longitude = 17.9},
                new Location {UserId = "user5", Latitude = 67.9, Longitude = 14.6},
                new Location {UserId = "user4", Latitude = 12.4, Longitude = 62.5},
                new Location {UserId = "user4", Latitude = 7.6, Longitude = 26.4},
                new Location {UserId = "user1", Latitude = 12.8, Longitude = 36.6},
                new Location {UserId = "user5", Latitude = 44.7, Longitude = 37.1},
                new Location {UserId = "user2", Latitude = 76.9, Longitude = 56.9},
                new Location {UserId = "user3", Latitude = 14.7, Longitude = 12.6},
                new Location {UserId = "user3", Latitude = 16.9, Longitude = 67.3},
                new Location {UserId = "user2", Latitude = 46.8, Longitude = 35.8},
                new Location {UserId = "user1", Latitude = 32.5, Longitude = 32.5},
                new Location {UserId = "user2", Latitude = 23.3, Longitude = 81.4},
                new Location {UserId = "user1", Latitude = 4.6, Longitude = 80.9}
            };
        }
    }
}
