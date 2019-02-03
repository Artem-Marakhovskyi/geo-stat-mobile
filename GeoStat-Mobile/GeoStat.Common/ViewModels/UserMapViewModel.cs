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
        private IMvxLocationWatcher _watcher;

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

            //Locations = locationService.GetLocationsOfUserAsync().Result;
            
            Locations = new List<LocationModel>
            {
                new LocationModel (0.0, 0.0, DateTimeOffset.Now),
                new LocationModel ( 45.7, 48.3, DateTimeOffset.Now),
                new LocationModel ( -15.0, 86.2, DateTimeOffset.Now),
                new LocationModel ( 17.0, -67.4, DateTimeOffset.Now)
            };

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

        //List<Location> _locations;
        public IEnumerable<LocationModel> Locations { get; }
    }
}
