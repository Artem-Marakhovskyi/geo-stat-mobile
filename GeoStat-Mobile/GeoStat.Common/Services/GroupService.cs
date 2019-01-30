using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GeoStat.Common.Abstractions;
using GeoStat.Common.Models;
using System.Linq;
namespace GeoStat.Common.Services
{
    public class GroupService
    {
        private readonly IGeoStatRepository<Group> _groupRepository;
        private readonly IGeoStatRepository<GroupUser> _groupUserRepository;
        private readonly IGeoStatRepository<GeoStatUser> _userRepository;
        private readonly IMapper _mapper;
        private readonly UserContext _userContext;

        public GroupService(
            IGeoStatRepository<Group> groupRepository,
            IGeoStatRepository<GroupUser> groupUserRepository,
            IGeoStatRepository<GeoStatUser> userRepository,
            IMapper mapper,
            UserContext userContext)
        {
            _groupRepository = groupRepository;
            _groupUserRepository = groupUserRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<GroupModel> CreateGroupAsync(GroupModel group)
        {
            if (group.CreatorId != _userContext.UserId)
                throw new ArgumentException();

            var createdGroup = await _groupRepository.UpsertItemAsync(Mapper.Map<Group>(group));

            var updatedGroupModel = _mapper.Map<GroupModel>(createdGroup);

            _userContext.AddGroup(updatedGroupModel);

            return updatedGroupModel;
        }

        public async Task DeleteGroupAsync(GroupModel group)
        {
            if (group.CreatorId != _userContext.UserId)
                throw new ArgumentException();

            await RemoveAllUsersOfGroupAsync(group.Id);

            var deletedGroup = await _groupRepository.ReadItemByIdAsync(group.Id);

            _userContext.RemoveGroup(deletedGroup.Id);

            await _groupRepository.DeleteItemAsync(deletedGroup);
        }

        public async Task<GroupModel> UpdateGroupLabelAsync(
            GroupModel group,
            string newLabel)
        {
            var needsToBeUpdatedGroup = await _groupRepository
                                        .ReadItemByIdAsync(group.Id);

            needsToBeUpdatedGroup.Label = newLabel;

            var updatedGroup = await _groupRepository
                                        .UpsertItemAsync(needsToBeUpdatedGroup);

            return _mapper.Map<GroupModel>(updatedGroup);
        }

        public async Task<ICollection<UserModel>> GetUsersOfGroupAsync(string groupId)
        {
            var query = await _groupUserRepository.CreateQuery();
            var groupUsersId = await query
                .Where(e => e.GroupId == groupId)
                .Select(u => u.UserId)
                .ToListAsync();

            var users = await _userRepository.CreateQuery();
            var usersOfGroup = await users
                .Where(u => groupUsersId.Contains(u.UserId))
                .ToListAsync();

            return new List<UserModel>(_mapper.Map<UserModel[]>(usersOfGroup));
        }

        public async Task RemoveAllUsersOfGroupAsync(string groupId)
        {
            var query = await _groupUserRepository.CreateQuery();
            var usersOfGroup = await query.Where(u => u.GroupId == groupId).ToListAsync();

            foreach (var user in usersOfGroup)
            {
                await _groupUserRepository.DeleteItemAsync(user);
            }
        }

        public async Task InviteUserAsync() => throw new NotImplementedException();
    }
}
