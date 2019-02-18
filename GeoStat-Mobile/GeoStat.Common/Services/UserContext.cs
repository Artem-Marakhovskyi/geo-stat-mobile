using System;
using System.Collections.Generic;
using GeoStat.Common.Models;
using System.Linq;
using GeoStat.Common.Abstractions;

namespace GeoStat.Common.Services
{
    public class UserContext
    {
        private readonly ICredentialsStorage _storageService;

        public string UserId => _storageService.GetUserId();
        public string UserEmail => _storageService.GetUserEmail();
        public string Token => _storageService.GetToken();
        public ICollection<GroupModel> Groups { get; private set; }

        public UserContext(ICredentialsStorage storageService)
        {
            _storageService = storageService;
            Groups = new List<GroupModel>();
        }

        public void AddGroup(GroupModel group)
        {
            if (Groups.Any(g => group.Id == g.Id))
                return;

            Groups.Add(group);
        }

        public void RemoveGroup(string groupId)
        {
            var sameGroup = Groups.FirstOrDefault(g => groupId == g.Id);

            if (sameGroup == null) return;

            Groups.Remove(sameGroup);
        }
    }
}
