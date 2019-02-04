using MvvmCross;
using MvvmCross.Plugin;
using GeoStat.Common.Locations;
using GeoStat.Droid.Services;

namespace GeoStat.Droid.LocationPlugin
{
    [MvxPlugin]
    public class LocationPlugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.IoCProvider.RegisterSingleton<ILocationJobStarter>(new LocationJobStarter());
            Mvx.IoCProvider.RegisterSingleton<ILocationFileManager>(new LocationFileManager());
        }
    }
}