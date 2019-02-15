using System.Collections.Generic;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using MvvmCross.Plugin.Location;
using System.Linq;
using MvvmCross.Navigation;
using MvvmCross.Logging;
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
        private readonly IMvxLog _log;
        private readonly LocationService _locationService;
        private readonly ICloudService _cloudService;

        public HomeViewModel(
            IMvxNavigationService navigationService,
            ILocationJobStarter locationJobStarter,
            ILocationFileManager locationFileManager,
            IMvxLog log,
            LocationService locationService,
            ICloudService cloudService)
        {
            _locationFileManager = locationFileManager;
            _navigationService = navigationService;
            _locationJobStarter = locationJobStarter;
            _log = log;
            _locationService = locationService;
            _cloudService = cloudService;
        }

        public async override void Start()
        {
            base.Start();

            var locations = _locationFileManager.ReadLocations();
            _locationFileManager.RemoveFile();
            _locationJobStarter.StartLocationJob(16 * 60 * 1000);

            LocationsCount = locations.Count();
            LatestLocation = "empty";

            await _cloudService.SyncOfflineCacheAsync();
            var locationsOfUser = await _locationService.GetLocationsOfUserAsync();
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
