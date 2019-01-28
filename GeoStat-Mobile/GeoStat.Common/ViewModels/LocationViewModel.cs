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
        public IMvxCommand SendLocationCommand => new MvxCommand(() => SendLocationAsync(12, 12));

        private readonly LocationService _locationService;

        public LocationViewModel(LocationService locationService)
        {
            _locationService = locationService;
        }

        private async void SendLocationAsync(int latitude, int longitude)
        {
            var locationModel = new LocationModel(latitude, longitude, DateTimeOffset.UtcNow);

            await _locationService.AddLocationAsync(locationModel);
        }

        private async Task<ICollection<LocationModel>> GetUserLocationsAsync()
        {
            return await _locationService.GetLocationsOfUserAsync();
        }

        private async Task<ICollection<LocationModel>> GetGroupLocationsAsync(string groupId)
        {
            return await _locationService.GetLocationsByGroupIdAsync(groupId);
        }
    }
}
