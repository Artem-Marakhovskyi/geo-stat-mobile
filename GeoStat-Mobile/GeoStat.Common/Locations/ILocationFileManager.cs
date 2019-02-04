using System.Collections.Generic;

namespace GeoStat.Common.Locations
{
    public interface ILocationFileManager
    {
        void AddLocation(LocationCoordinate location);
        void RemoveFile();
        void CreateFileIfNotExists();
        IEnumerable<LocationCoordinate> ReadLocations();
    }
}