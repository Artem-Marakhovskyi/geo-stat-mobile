using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using MvvmCross.Plugin.Location;
using MvvmCross;
using GeoStat.Common.Services;
using System.Linq;
using System.Collections.ObjectModel;
using MvvmCross.Navigation;
using MvvmCross.Logging;
using GeoStat.Common.Locations;

namespace GeoStat.Common.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        private readonly ILocationFileManager _locationFileManager;
        private readonly IMvxNavigationService _navigationService;
        private readonly IMvxLog _log;

        public HomeViewModel(
            IMvxNavigationService navigationService, 
            ILocationFileManager locationFileManager,
            IMvxLog log)
        {
            _locationFileManager = locationFileManager;
            _navigationService = navigationService;
            _log = log;
        }

        public override void Start()
        {
            base.Start();

            var locations = _locationFileManager.ReadLocations();

            LocationsCount = locations.Count();
            LatestLocation = "empty";
        }

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

        IEnumerable<string> _locations;

        private string _l;
        public string LatestLocation
        {
            get { return _l; }
            set { _l = value; RaisePropertyChanged(() => LatestLocation); }
        }

        private int _count;
        public int LocationsCount
        {
            get { return _count; }
            set { _count = value; RaisePropertyChanged(() => LocationsCount); }
        }

        public IMvxCommand ShowMapCommand => new MvxCommand(ShowMap);

        private void ShowMap()
        {
            _navigationService.Navigate<MapViewModel>();
        }

    }
}
