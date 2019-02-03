using System;
using System.Collections.Generic;
using System.Text;
using GeoStat.Common.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using GeoStat.Common.Services;

namespace GeoStat.Common.ViewModels
{
    public class GroupMapViewModel : MvxViewModel<GroupModel>
    {
        public GroupModel CurrentGroup { get; set; }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(() => Title); }
        }

        public IEnumerable<UserModel> GroupMembers { get; set; }
        
        public IEnumerable<LocationModel> GroupLocations { get; set; }

        public GroupMapViewModel(LocationService locationService,
                                 GroupService groupService)
        {
            

            //GroupLocations = locationService.GetLocationsByGroupIdAsync(groupId).Result;
            //GroupMembers = groupService.GetUsersOfGroupAsync(groupId).Result;

        }

        

        public override void Prepare(GroupModel group)
        {
            CurrentGroup = group;

            Title = $"Group {CurrentGroup.Id} Map";
            // Add test group
            GroupMembers = new List<UserModel>
            {
                new UserModel { UserId = "user1" },
                new UserModel { UserId = "user2" },
                new UserModel { UserId = "user3" },
                new UserModel { UserId = "user4" },
                new UserModel { UserId = "user5" }
            };

            GroupLocations = new List<LocationModel>
            {
                new LocationModel( 7.3, 67.2, DateTimeOffset.Now, "user1"),
                new LocationModel (0.3, 0.2, DateTimeOffset.Now, "user2"),
                new LocationModel (12.4, 12.5, DateTimeOffset.Now, "user3"),
                new LocationModel (34.7, 17.9, DateTimeOffset.Now,"user1"),
                new LocationModel (67.9, 14.6, DateTimeOffset.Now, "user5"),
                new LocationModel (12.4,  62.5, DateTimeOffset.Now, "user4"),
                new LocationModel (7.6, 26.4, DateTimeOffset.Now, "user4"),
                new LocationModel (12.8, 36.6, DateTimeOffset.Now, "user1"),
                new LocationModel (44.7, 37.1, DateTimeOffset.Now, "user5"),
                new LocationModel (76.9, 56.9, DateTimeOffset.Now,"user2"),
                new LocationModel (14.7, 12.6, DateTimeOffset.Now, "user3"),
                new LocationModel (16.9, 67.3, DateTimeOffset.Now, "user3"),
                new LocationModel (46.8, 35.8, DateTimeOffset.Now, "user2"),
                new LocationModel (32.5, 32.5, DateTimeOffset.Now,"user1"),
                new LocationModel (23.3, 81.4, DateTimeOffset.Now, "user2"),
                new LocationModel (4.6, 80.9, DateTimeOffset.Now, "user1")
            };
        }
    }
}
