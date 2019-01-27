using System;
using System.Collections.Generic;
using System.Text;

using MvvmCross.Commands;
using MvvmCross.ViewModels;
using MvvmCross.Plugin.Location;

namespace GeoStat.Common.ViewModels
{
    public class MapViewModel : MvxViewModel
    {
        //IMvxLocationWatcher _watcher;

        public MapViewModel()
        {

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
