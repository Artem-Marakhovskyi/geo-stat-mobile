using System;
using System.Collections.Generic;
using System.Text;
using GeoStat.Common.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using MvvmCross.Plugin.Location;

namespace GeoStat.Common.ViewModels
{
    public class MapViewModel : MvxViewModel
    {
        IMvxLocationWatcher _watcher;

        public MapViewModel(IMvxLocationWatcher watcher)
        {
            _watcher = watcher;
            Lng = _watcher.CurrentLocation.Coordinates.Longitude;
            Lat = _watcher.CurrentLocation.Coordinates.Latitude;

            _locations = new List<Location>();
            _locations.Add(new Location { Latitude = 0.0, Longitude = 0.0 });
            _locations.Add(new Location { Latitude = 90.0, Longitude = 90.0 });
            _locations.Add(new Location { Latitude = 45.7, Longitude = 48.3 });
            _locations.Add(new Location { Latitude = -15.0, Longitude = 86.2 });
            _locations.Add(new Location { Latitude = 17.0, Longitude = -67.4 });

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

        double minLat = -90.0;
        double maxLat = 90;
        double minLng = -180.0;
        double maxLng = 180.0;

        Location RandLocation()
        {
            Location l = new Location();
            l.Latitude = GetRandomLat();
            l.Longitude = GetRandomLng();
            return l;
        }

        public double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public double GetRandomLat()
        {
            return GetRandomNumber(minLat, maxLat);
        }

        public double GetRandomLng()
        {
            return GetRandomNumber(minLng, maxLng);
        }

        List<Location> _locations;
        public IEnumerable<Location> Locations
        { get { return _locations; } }

    }
}
