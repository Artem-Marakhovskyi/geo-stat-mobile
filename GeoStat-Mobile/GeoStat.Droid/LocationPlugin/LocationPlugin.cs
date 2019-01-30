using System;
using MvvmCross;
using MvvmCross.Plugin;
using MvvmCross.Commands;

using GeoStat.Common.Services;
using GeoStat.Droid.Services;

namespace GeoStat.Droid.LocationPlugin
{
    [MvxPlugin]
    public class LocationPlugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.IoCProvider.RegisterSingleton<ILocationService>(new LocationService());
        }
    }
}