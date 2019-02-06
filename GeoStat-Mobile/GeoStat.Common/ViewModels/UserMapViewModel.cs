using System;
using System.Collections.Generic;
using System.Text;
using GeoStat.Common.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using MvvmCross.Plugin.Location;
using GeoStat.Common.Services;

namespace GeoStat.Common.ViewModels
{
    public class UserMapViewModel : MvxViewModel
    {
        private readonly IMvxLocationWatcher _watcher;
        private readonly LocationService _locationService;

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(() => Title); }
        }

        public UserMapViewModel(LocationService locationService,
                                IMvxLocationWatcher watcher)
        {
            _watcher = watcher;
            Lng = _watcher.CurrentLocation.Coordinates.Longitude;
            Lat = _watcher.CurrentLocation.Coordinates.Latitude;

            Title = "User Map";
        }

        public async override void Start()
        {
            base.Start();
            Locations = await _locationService.GetLocationsOfUserAsync();
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

        public IEnumerable<LocationModel> Locations { get; set; }
    }
}
