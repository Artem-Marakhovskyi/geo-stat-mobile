using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeoStat.Common.Abstractions;
using GeoStat.Common.Models;
using AutoMapper;

namespace GeoStat.Common.Services
{
    public class UserService
    {
        private readonly IGeoStatRepository<GeoStatUser> _userRepository;
        private readonly IGeoStatRepository<Group> _groupRepository;
        private readonly IGeoStatRepository<GroupUser> _groupUserRepository;
        private readonly UserContext _userContext;
        private readonly IMapper _mapper;

        public UserService(
            IGeoStatRepository<GeoStatUser> userRepository,
            IGeoStatRepository<Group> groupRepository,
            IGeoStatRepository<GroupUser> groupUserRepository,
            UserContext userContext,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _groupUserRepository = groupUserRepository;
            _userContext = userContext;
            _mapper = mapper;
        }

        public Task<UserModel> Register()
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> Login()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<GroupModel>> GetGroupsOfUser()
        {
            var groupUserQuery = await _groupUserRepository.CreateQuery();
            var groupIds = await groupUserQuery
                .Where(u => u.UserId == _userContext.UserId)
                .Select(g => g.GroupId)
                .ToListAsync();

            var groupQuery = await _groupRepository.CreateQuery();
            var groups = await groupQuery
                .Where(g => groupIds.Contains(g.Id))
                .ToListAsync();

            return new List<GroupModel>(_mapper.Map<GroupModel[]>(groups));
        }
    }
}

