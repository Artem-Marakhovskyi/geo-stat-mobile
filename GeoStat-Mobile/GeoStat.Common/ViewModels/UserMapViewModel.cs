using System;
using System.Collections.Generic;
using System.Text;
using GeoStat.Common.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using MvvmCross.Plugin.Location;

namespace GeoStat.Common.ViewModels
{
    public class UserMapViewModel : MvxViewModel
    {
        private IMvxLocationWatcher _watcher;

        public UserMapViewModel(IMvxLocationWatcher watcher)
        {
            _watcher = watcher;
            Lng = _watcher.CurrentLocation.Coordinates.Longitude;
            Lat = _watcher.CurrentLocation.Coordinates.Latitude;

            _locations = new List<Location>
            {
                new Location { Latitude = 0.0, Longitude = 0.0 },
                new Location { Latitude = 45.7, Longitude = 48.3 },
                new Location { Latitude = -15.0, Longitude = 86.2 },
                new Location { Latitude = 17.0, Longitude = -67.4 }
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

        List<Location> _locations;
        public IEnumerable<Location> Locations
        { get { return _locations; } }

    }
}
