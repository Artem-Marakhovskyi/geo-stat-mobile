using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeoStat.Common.Abstractions;
using GeoStat.Common.Models;
using GeoStat.Common.Services;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace GeoStat.Common.ViewModels
{
    public class LocationViewModel : MvxViewModel
    {
        private readonly LocationService _locationService;

        public LocationViewModel(LocationService locationService)
        {
            _locationService = locationService;
        }

        private Task SendLocationAsync(double latitude = 0, double longitude = 0)
        {
            var locationModel = new LocationModel(latitude, longitude, DateTimeOffset.UtcNow);

            return _locationService.AddLocationAsync(locationModel);
        }

        private Task<IEnumerable<LocationModel>> GetUserLocationsAsync()
        {
            return _locationService.GetLocationsOfUserAsync();
        }

        private Task<IEnumerable<LocationModel>> GetGroupLocationsAsync(string groupId)
        {
            return _locationService.GetLocationsByGroupIdAsync(groupId);
        }
    }
}
