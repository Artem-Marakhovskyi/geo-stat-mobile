using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Plugin.Location;

namespace GeoStat.Common.Locations
{
    public class LocationFileManager : ILocationFileManager
    {
        private const string FileName = "locations.txt";

        private Lazy<string> FullPath => new Lazy<string>(
            () =>
                Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.Personal),
                    FileName));

        public void RemoveFile()
        {
            if (File.Exists(FullPath.Value))
            {
                File.Delete(FullPath.Value);
            }
        }

        public void AddLocation(MvxGeoLocation location)
        {
            var info = $"{DateTime.Now},{location.Coordinates.Latitude},{location.Coordinates.Longitude}{Environment.NewLine}";

            using (var writer = new StreamWriter(FullPath.Value, true))
            {
                writer.WriteLine(info);
            }
        }

        public IEnumerable<LocationCoordinate> ReadLocations()
        {
            if (!File.Exists(FullPath.Value))
            {
                return Enumerable.Empty<LocationCoordinate>();
            }

            try
            {
                var lines = new List<string>();
                using (var reader = new StreamReader(FullPath.Value))
                {
                    lines.Add(reader.ReadLine());
                }
                var coords = new LocationCoordinate[lines.Count];
                for (var i = 0; i < lines.Count; i++)
                {
                    coords[i] = LocationCoordinate.From(lines[i]);
                }

                return coords;

            }
            catch (Exception ex)
            {
                return Enumerable.Empty<LocationCoordinate>();
            }
        }

        public void CreateFileIfNotExists()
        {
            if (!File.Exists(FullPath.Value))
            {
                File.Create(FullPath.Value);
            }
        }

        public void AddLine(string l)
        {

            using (var writer = new StreamWriter(FullPath.Value, true))
            {
                writer.WriteLine(l);
            }
        }
    }
}
