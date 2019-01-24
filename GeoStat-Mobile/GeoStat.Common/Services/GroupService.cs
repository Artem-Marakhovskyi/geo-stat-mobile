using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeoStat.Common.Abstractions;
using GeoStat.Common.Models;

namespace GeoStat.Common.Services
{
    public class GroupService
    {
        private readonly IGeoStatRepository<Group> _groupRepository;
        private readonly IGeoStatRepository<GroupUser> _groupUserRepository;

        public GroupService(IGeoStatRepository<Group> groupRepository,
                            IGeoStatRepository<GroupUser> groupUserRepository)
        {
            _groupRepository = groupRepository;
            _groupUserRepository = groupUserRepository;
        }

        public async Task<GroupModel> CreateGroupAsync(GroupModel group)
        {
            if (group.CreatorId != UserContext.UserId)
                throw new ArgumentException();

            var createdGroup = await _groupRepository.UpsertItemAsync(new Group
            {
                Label = group.Label,
                CreatorId = group.CreatorId
            });

            var updatedGroupModel = new GroupModel(createdGroup.Id,
                                                    createdGroup.Label,
                                                    createdGroup.CreatorId);

            UserContext.AddGroup(updatedGroupModel);

            return updatedGroupModel;
        }

        public async Task DeleteGroupAsync(GroupModel group)
        {
            if (group.CreatorId != UserContext.UserId)
                throw new ArgumentException();

            await RemoveAllUsersAsync(group.GroupId);

            var deletedGroup = await _groupRepository.ReadItemByIdAsync(group.GroupId);

            UserContext.RemoveGroup(deletedGroup.Id);

            await _groupRepository.DeleteItemAsync(deletedGroup);
        }

        public async Task<GroupModel> UpdateGroupLabelAsync(GroupModel group, string newLabel)
        {
            var needsToBeUpdatedGroup = await _groupRepository
                                        .ReadItemByIdAsync(group.GroupId);

            needsToBeUpdatedGroup.Label = newLabel;

            var updatedGroup = await _groupRepository
                                        .UpsertItemAsync(needsToBeUpdatedGroup);

            return new GroupModel(updatedGroup.Id,
                                   updatedGroup.Label,
                                   updatedGroup.CreatorId
                                  );
        }

        //public async Task<ICollection<UserModel>> GetUsersOfGroupAsync(string groupId)
        //{
        //    //need user repository???
        //}

        //public async Task InviteUserAsync() => throw new NotImplementedException();

        //public async Task RemoveUserFromGroupAsync()
        //{ }

        public async Task RemoveAllUsersAsync(string groupId)
        {
            var groupUsers = await _groupUserRepository.ReadAllItemsAsync();

            foreach (var user in groupUsers)
            {
                if (user.GroupId == groupId)
                    await _groupUserRepository.DeleteItemAsync(user);
            }
        }



    }
}
