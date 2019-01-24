using System;
using System.Collections.Generic;
using GeoStat.Common.Services;

namespace GeoStat.Common.Models
{
    public class GroupModel
    {
        public string GroupId { get; private set; }

        public string Label { get; private set; }

        public string CreatorId { get; private set; }

        public GroupModel(string groupId, string label, string creatorId)
        {
            GroupId = groupId;
            Label = label;
            CreatorId = creatorId;
        }
    }
}
