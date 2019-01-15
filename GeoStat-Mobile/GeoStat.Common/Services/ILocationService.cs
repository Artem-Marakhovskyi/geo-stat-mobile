using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Plugin.Location;

namespace GeoStat.Common.Services
{
    public interface ILocationService
    {
        // start service 
        MvxGeoLocation GetCurrentLocation(IMvxLocationWatcher watcher);

        void StartLocationService(int period);
    }
}
