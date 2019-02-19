using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeoStat.Common.Models;

namespace GeoStat.Common.Abstractions
{
    public interface ILocationService
    {
        Task<Location> AddLocationAsync(LocationModel locationModel);

        Task<IEnumerable<LocationModel>> GetLocationsOfUserAsync();
        Task<IEnumerable<LocationModel>> GetLocationsByGroupIdAsync(string id);
        Task<IEnumerable<LocationModel>> GetLocationsByDateAsync(DateTime dateTime);
    }
}
