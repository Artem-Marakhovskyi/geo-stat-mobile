using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeoStat.Common.Abstractions;
using GeoStat.Common.Models;
using System.Linq;
using AutoMapper;
using MvvmCross;

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
            return await GetLocationsByUserIdAsyc(_userContext.UserId);
        }

        public async Task<ICollection<LocationModel>> GetLocationsByGroupIdAsync(string id)
        {
            var groupUserQuery = await _groupUserRepository.CreateQuery();

            var groupUsers = await groupUserQuery.Where(g => g.GroupId == id).ToListAsync();
            var locationsList = new List<LocationModel>();

            foreach (var user in groupUsers)
                locationsList.AddRange(await GetLocationsByUserIdAsyc(user.UserId));

            return locationsList;
        }

        private async Task<ICollection<LocationModel>> GetLocationsByUserIdAsyc(string id)
        {
            var query = await _locationRepository.CreateQuery();
            var locations = await query.Where(l => l.Id == id).ToListAsync();
            var locationModelList = new List<LocationModel>();

            foreach (var item in locations)
                locationModelList.Add(_mapper.Map<LocationModel>(item));

            return locationModelList;
        }
    }
}
