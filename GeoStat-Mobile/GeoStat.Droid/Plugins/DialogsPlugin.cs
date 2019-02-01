using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.Plugin;

namespace GeoStat.Droid.Plugins
{
    [MvxPlugin]
    public class DialogsPlugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.IoCProvider.RegisterSingleton<IUserDialogs>(UserDialogs.Instance);
        }
    }
}
