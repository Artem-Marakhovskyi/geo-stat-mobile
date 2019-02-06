﻿using System;
using System.Collections.Generic;
using GeoStat.Common.Models;
using System.Linq;

namespace GeoStat.Common.Services
{
    public class UserContext
    {
        public string UserId { get; private set; } = "0bea1ec51d73494c95b55941b27ca497";

        public ICollection<GroupModel> Groups { get; private set; }

        public UserContext()
        {
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
