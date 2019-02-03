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

        public void AddLocation(LocationCoordinate location)
        {
            var info = $"{DateTime.Now},{location.Latitude},{location.Longitude}{Environment.NewLine}";

            using (var stream = new FileStream(FullPath.Value, FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(info);
                }
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
                using (var stream = new FileStream(FullPath.Value, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        lines.Add(reader.ReadLine());
                    }
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
                File.Create(FullPath.Value).Close();
            }
        }
    }
}
