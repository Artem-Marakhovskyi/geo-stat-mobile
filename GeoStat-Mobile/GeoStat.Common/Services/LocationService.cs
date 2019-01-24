using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeoStat.Common.Abstractions;
using GeoStat.Common.Models;
using System.Linq;

namespace GeoStat.Common.Services
{
    public class LocationService
    {
        private readonly IGeoStatRepository<Location> _locationRepository;

        public LocationService(IGeoStatRepository<Location> repository)
        {
            _locationRepository = repository;
        }

        public async Task<Location> AddLocationAsync(LocationModel location)
        {
            return await _locationRepository.UpsertItemAsync(new Location
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                DateTime = location.DateTime,
                UserId = UserContext.UserId
            });
        }

        public async Task<ICollection<LocationModel>> GetLocationsByIdAsync(string id)
        {
            var locations = await _locationRepository.ReadAllItemsAsync();

            var selectedLocations = locations.Where(location => location.UserId == id);
            var locationModels = new List<LocationModel>();

            foreach (var item in selectedLocations)
                locationModels.Add(new LocationModel(item.Latitude,
                                                       item.Longitude,
                                                       item.DateTime,
                                                       item.UserId));

            return locationModels;
        }
    }
}
