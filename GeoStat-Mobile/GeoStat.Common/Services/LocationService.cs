using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeoStat.Common.Abstractions;
using GeoStat.Common.Models;
using System.Linq;
using AutoMapper;
using System.Linq.Expressions;

namespace GeoStat.Common.Services
{
    public class LocationService
    {
        private readonly IGeoStatRepository<Location> _locationRepository;
        private readonly IGeoStatRepository<GroupUser> _groupUserRepository;
        private readonly UserContext _userContext;
        private readonly IMapper _mapper;

        public LocationService(IGeoStatRepository<Location> locationRepository,
                               IGeoStatRepository<GroupUser> groupUserRepository,
                               IMapper mapper,
                               UserContext userContext)
        {
            _locationRepository = locationRepository;
            _groupUserRepository = groupUserRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<Location> AddLocationAsync(LocationModel locationModel)
        {
            var location = _mapper.Map<Location>(locationModel);
            location.UserId = _userContext.UserId;

            return await _locationRepository.UpsertItemAsync(location);
        }

        public async Task<ICollection<LocationModel>> GetLocationsOfUserAsync()
        {
            return await GetLocationsByUserIdAsync(_userContext.UserId);
        }

        public async Task<ICollection<LocationModel>> GetLocationsByGroupIdAsync(string id)
        {
            var groupUserQuery = await _groupUserRepository.CreateQuery();

            var groupUsers = await groupUserQuery.Where(g => g.GroupId == id).ToListAsync();
            var locationsList = new List<LocationModel>();

            foreach (var user in groupUsers)
            {
                locationsList.AddRange(await GetLocationsByUserIdAsync(user.UserId));
            }

            return locationsList;
        }

        public async Task<ICollection<LocationModel>> GetLocationsByDateAsync(DateTime dateTime)
        {
            return await GetLocationsByQueryAsync(l => l.DateTime > dateTime);
        }

        private async Task<ICollection<LocationModel>> GetLocationsByUserIdAsync(string id)
        {
            return await GetLocationsByQueryAsync(l => l.UserId == id);
        }

        private async Task<ICollection<LocationModel>> GetLocationsByQueryAsync(
            Expression<Func<Location, bool>> predicate)
        {
            var query = await _locationRepository.CreateQuery();
            var locations = await query.Where(predicate).ToListAsync();

            return new List<LocationModel>(_mapper.Map<LocationModel[]>(locations));
        }
    }
}
