﻿using System.Collections.Generic;
using MvvmCross.Commands;
using System.Windows.Input;
using MvvmCross.ViewModels;
using MvvmCross.Plugin.Location;
using System.Linq;
using MvvmCross.Navigation;
using MvvmCross.Logging;
using GeoStat.Common.Models;
using GeoStat.Common.Locations;
using GeoStat.Common.Services;
using GeoStat.Common.Abstractions;

namespace GeoStat.Common.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        private readonly ILocationFileManager _locationFileManager;
        private readonly IMvxNavigationService _navigationService;
        private readonly ILocationJobStarter _locationJobStarter;
        private readonly UserService _userService;
        private readonly LocationService _locationService;
        private readonly IMvxLog _log;
        private readonly ICloudService _cloudService;

        public HomeViewModel(
            IMvxNavigationService navigationService, 
            ILocationJobStarter locationJobStarter,
            ILocationFileManager locationFileManager,
            IMvxLog log,
            UserService userService,
            LocationService locationService,
            ICloudService cloudService)
        {
            _locationFileManager = locationFileManager;
            _navigationService = navigationService;
            _locationJobStarter = locationJobStarter;
            _log = log;
            _userService = userService;
            _locationService = locationService;
            _cloudService = cloudService;

            /*Groups = new List<GroupModel>
            {
                new GroupModel ("group1", "first group", "user1"),
                new GroupModel ("group2", "second group", "user1"),
                new GroupModel ("group3", "third group", "user1")
            };*/
        }

        public async override void Start()
        {
            base.Start();

            var locations = _locationFileManager.ReadLocations();

            _locationService.AddStoredLocations(locations);
            _cloudService.SyncOfflineCacheAsync();
            
            _locationFileManager.RemoveFile();
            _locationJobStarter.StartLocationJob(16 * 60 * 1000);

            Groups = await _userService.GetGroupsOfUser();

            //LocationsCount = locations.Count();
            //LatestLocation = "empty";
        }
        /*
        public void OnLocation(MvxGeoLocation location)
        {
            Lat = location.Coordinates.Latitude;
            Lng = location.Coordinates.Longitude;
        }

        public void OnError(MvxLocationError error)
        {
            _log.Error(error.Code.ToString()); 
        }

        private double _lng;
        public double Lng
        {
            get { return _lng; }
            set { _lng = value; RaisePropertyChanged(() => Lng); }
        }

        private double _lat;
        public double Lat
        {
            get { return _lat; }
            set { _lat = value; RaisePropertyChanged(() => Lat); }
        }
        */
        public IMvxCommand ShowUserMapCommand => new MvxCommand(ShowUserMap);

        private void ShowUserMap()
        {
            _navigationService.Navigate<UserMapViewModel>();
        }

        public IEnumerable<GroupModel> Groups { get; set; }

        public void ShowGroupMapById(GroupModel group)
        {
            _navigationService.Navigate<GroupMapViewModel, GroupModel>(group);
        }

        public IMvxCommand ShowGroupByIdCommand => new MvxCommand<GroupModel>((group) => ShowGroupMapById(group));

    }
}
