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
        private readonly GroupService _groupService;
        private readonly LocationService _locationService;

        public GroupModel CurrentGroup { get; set; }
        public IEnumerable<UserModel> GroupMembers { get; set; }
        public IEnumerable<LocationModel> GroupLocations { get; set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(() => Title); }
        }

        public GroupMapViewModel(LocationService locationService,
                                 GroupService groupService)
        {
            _locationService = locationService;
            _groupService = groupService;
        }    

        public async override void Prepare(GroupModel group)
        {
            CurrentGroup = group;
            Title = $"{CurrentGroup.Label} Map";
          
            GroupLocations = await _locationService.GetLocationsByGroupIdAsync(CurrentGroup.Id);
            GroupMembers = await _groupService.GetUsersOfGroupAsync(CurrentGroup.Id);
        }
    }
}
