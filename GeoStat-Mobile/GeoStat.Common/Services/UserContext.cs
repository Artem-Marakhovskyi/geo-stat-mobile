using System;
using System.Collections.Generic;
using GeoStat.Common.Models;
using System.Linq;

namespace GeoStat.Common.Services
{
    public class UserContext
    {
        public static string UserId { get; private set; } = "f0df1b191f7545a9aac563c237b66727";

        public static ICollection<GroupModel> Groups { get; private set; }

        public UserContext()
        {
        }

        public static void AddGroup(GroupModel group)
        {
            if (Groups.Any(g => group.GroupId == g.GroupId))
                return;

            Groups.Add(group);
        }

        public static void RemoveGroup(string groupId)
        {
            var sameGroup = Groups.FirstOrDefault(g => groupId == g.GroupId);

            if (sameGroup == null) return;

            Groups.Remove(sameGroup);
        }
    }
}
