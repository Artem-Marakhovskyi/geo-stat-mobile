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

        public LocationService(
            IGeoStatRepository<Location> locationRepository,
            IGeoStatRepository<GroupUser> groupUserRepository,
            IMapper mapper,
            UserContext userContext)
        {
            _locationRepository = locationRepository;
            _groupUserRepository = groupUserRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public Task<Location> AddLocationAsync(LocationModel locationModel)
        {
            var location = _mapper.Map<Location>(locationModel);
            location.UserId = _userContext.UserId;

            return _locationRepository.UpsertItemAsync(location);
        }

        public Task<ICollection<LocationModel>> GetLocationsOfUserAsync()
        {
            return GetLocationsByUserIdAsync(_userContext.UserId);
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

        public Task<ICollection<LocationModel>> GetLocationsByDateAsync(DateTime dateTime)
        {
            return GetLocationsByQueryAsync(l => l.DateTime > dateTime);
        }

        private Task<ICollection<LocationModel>> GetLocationsByUserIdAsync(string id)
        {
            return GetLocationsByQueryAsync(l => l.UserId == id);
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
