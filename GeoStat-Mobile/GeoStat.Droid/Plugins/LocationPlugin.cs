using MvvmCross;
using MvvmCross.Plugin;

using GeoStat.Common.Services;

namespace GeoStat.Droid.LocationPlugin
{
    [MvxPlugin]
    public class LocationPlugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.IoCProvider.RegisterSingleton<ILocationService>(new Services.LocationService());
        }
    }
}