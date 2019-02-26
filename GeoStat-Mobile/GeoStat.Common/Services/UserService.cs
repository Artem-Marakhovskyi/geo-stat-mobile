using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeoStat.Common.Abstractions;
using GeoStat.Common.Models;
using AutoMapper;

namespace GeoStat.Common.Services
{
    public class UserService : IUserService
    {
        private readonly IGeoStatRepository<GeoStatUser> _userRepository;
        private readonly IGeoStatRepository<Group> _groupRepository;
        private readonly IGeoStatRepository<GroupUser> _groupUserRepository;
        private readonly UserContext _userContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;

        public UserService(
            IGeoStatRepository<GeoStatUser> userRepository,
            IGeoStatRepository<Group> groupRepository,
            IGeoStatRepository<GroupUser> groupUserRepository,
            UserContext userContext,
            IMapper mapper,
            IAuthorizationService authorizationService)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _groupUserRepository = groupUserRepository;
            _userContext = userContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        public Task Register(RegisterModel model)
        {
            return _authorizationService.SendRequestForRegister(model);
        }

        public Task Login(LoginModel model)
        {
            return _authorizationService.SendRequestForLogin(model);
        }

        public async Task<ICollection<GroupModel>> GetGroupsOfUser()
        {
            var groupUserQuery = await _groupUserRepository.CreateQuery();
            var groupIds = await groupUserQuery
                .Where(u => u.UserId == _userContext.UserId)
                .Select(g => g.GroupId)
                .ToListAsync();

            if (groupIds.Count == 0)
            {
                return new List<GroupModel>();
            }

            var groupQuery = await _groupRepository.CreateQuery();
            var groups = await groupQuery
                .Where(g => groupIds.Contains(g.Id))
                .ToListAsync();

            return new List<GroupModel>(_mapper.Map<GroupModel[]>(groups));
        }
    }
}

