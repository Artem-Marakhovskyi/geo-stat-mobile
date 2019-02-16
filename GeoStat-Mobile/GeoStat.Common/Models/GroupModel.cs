using System;
using System.Collections.Generic;
using GeoStat.Common.Services;

namespace GeoStat.Common.Models
{
    public class GroupModel
    {
        public string Id { get; private set; }

        public string Label { get; private set; }

        public string CreatorId { get; private set; }

        public GroupModel() { }

        public GroupModel(string label, string creatorId)
        {
            Label = label;
            CreatorId = creatorId;
        }

        public GroupModel(string groupId, string label, string creatorId) : this(label, creatorId)
        {
            Id = groupId;
        }
    }
}
