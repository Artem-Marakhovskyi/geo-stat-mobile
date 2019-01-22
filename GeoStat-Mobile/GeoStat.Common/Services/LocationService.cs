using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeoStat.Common.Abstractions;
using GeoStat.Common.Models;

namespace GeoStat_Mobile.Services
{
    public class LocationService
    {
        private readonly IGeoStatRepository<Location> _locationRepository;

        public LocationService(IGeoStatRepository<Location> repository)
        {
            _locationRepository = repository;
        }

        public async Task<ICollection<Location>> GetLocationsByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
