using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Plugin.Location;

namespace GeoStat.Common.Services
{
    public interface ILocationService
    {
        void StartLocationService(int period);

        IEnumerable<string> GetLocations();
    }
}
