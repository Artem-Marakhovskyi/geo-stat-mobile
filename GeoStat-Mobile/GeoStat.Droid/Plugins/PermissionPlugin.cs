using System;
using MvvmCross;
using MvvmCross.Plugin;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace GeoStat.Droid.Plugins
{
    [MvxPlugin]
    public class LocationPlugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.IoCProvider.RegisterSingleton<IPermissions>(new PermissionsImplementation());
        }
    }
}
