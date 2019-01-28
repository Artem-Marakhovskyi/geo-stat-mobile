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

        public GroupService(IGeoStatRepository<Group> groupRepository,
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

        public async Task<GroupModel> UpdateGroupLabelAsync(GroupModel group,
                                                            string newLabel)
        {
            var needsToBeUpdatedGroup = await _groupRepository
                                        .ReadItemByIdAsync(group.Id);

            needsToBeUpdatedGroup.Label = newLabel;

            var updatedGroup = await _groupRepository
                                        .UpsertItemAsync(needsToBeUpdatedGroup);

            return _mapper.Map<GroupModel>(updatedGroup);
        }

        public async Task<List<UserModel>> GetUsersOfGroupAsync(string groupId)
        {
            var query = await _groupUserRepository.CreateQuery();
            var groupUsers = await query.Where(e => e.GroupId == groupId).ToListAsync();
            var userModelList = new List<UserModel>();

            foreach (var user in groupUsers)
            {
                var model = _userRepository.ReadItemByIdAsync(user.UserId);
                userModelList.Add(_mapper.Map<UserModel>(model));
            }

            return userModelList;
        }

        public async Task RemoveAllUsersOfGroupAsync(string groupId)
        {
            var groupUsers = await _groupUserRepository.ReadAllItemsAsync();

            foreach (var user in groupUsers)
            {
                if (user.GroupId == groupId)
                    await _groupUserRepository.DeleteItemAsync(user);
            }
        }

        public async Task InviteUserAsync() => throw new NotImplementedException();
    }
}
