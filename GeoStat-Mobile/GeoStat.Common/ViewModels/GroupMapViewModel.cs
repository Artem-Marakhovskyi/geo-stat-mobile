using System;
using System.Collections.Generic;
using System.Text;
using GeoStat.Common.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using GeoStat.Common.Services;
using GeoStat.Common.Abstractions;
using System.Threading.Tasks;

namespace GeoStat.Common.ViewModels
{
    public class GroupMapViewModel : MvxViewModel<GroupModel>
    {
        private readonly GroupService _groupService;
        private readonly ILocationService _locationService;

        public GroupModel CurrentGroup { get; set; }

        public IEnumerable<UserModel> GroupMembers { get; set; }
        public IEnumerable<LocationModel> GroupLocations { get; set; }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public GroupMapViewModel(ILocationService locationService,
                                 GroupService groupService)
        {
            _locationService = locationService;
            _groupService = groupService;
        }    

        public async override void Prepare(GroupModel group)
        {
            CurrentGroup = group;
            Title = $"{CurrentGroup.Label} Map";

            GroupMembers = await _groupService.GetUsersOfGroupAsync(CurrentGroup.Id);
            GroupLocations = await _locationService.GetLocationsByGroupIdAsync(CurrentGroup.Id);
        }
    }
}
