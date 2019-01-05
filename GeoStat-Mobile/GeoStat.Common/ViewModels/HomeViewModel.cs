using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using MvvmCross.Plugin.Location;
using MvvmCross;

namespace GeoStat.Common.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        IMvxLocationWatcher _watcher;
        public HomeViewModel(IMvxLocationWatcher watcher)
        {
            _watcher = watcher;
            _watcher.Start(new MvxLocationOptions(), OnLocation, OnError);
        }

        public void OnLocation(MvxGeoLocation location)
        {
            Lat = location.Coordinates.Latitude;
            Lng = location.Coordinates.Longitude;
        }

        public void OnError(MvxLocationError error)
        {
            throw new NotImplementedException();
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
    }
}
