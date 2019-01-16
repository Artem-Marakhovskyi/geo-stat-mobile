using System;
using MvvmCross;
using MvvmCross.Plugin;
using MvvmCross.Commands;

namespace GeoStat.GeoLocationPlugin
{
    [MvxPluginAttribute]
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            //Mvx.IoCProvider.RegisterSingleton<ILocationService>(new LocationService());
        }
    }
}
