﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Plugin.Location;

namespace GeoStat.Common.Locations
{
    public interface ILocationFileManager
    {
        void AddLocation(MvxGeoLocation location);
        void RemoveFile();
        void CreateFileIfNotExists();
        IEnumerable<LocationCoordinate> ReadLocations();
    }
}