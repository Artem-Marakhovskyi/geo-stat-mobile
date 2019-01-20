using MvvmCross.IoC;
using MvvmCross.ViewModels;
using GeoStat.Common.ViewModels;
using GeoStat.Common.Abstractions;
using GeoStat.Common.Services;
using MvvmCross;

namespace GeoStat.Common
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            RegisterAppStart<HomeViewModel>();

            Mvx.IoCProvider.RegisterType<ICloudService, AzureCloudService>();
        }
    }
}
