using MvvmCross.IoC;
using MvvmCross.ViewModels;
using GeoStat.Common.ViewModels;
using GeoStat.Common.Abstractions;
using GeoStat.Common.Services;
using MvvmCross;
using Microsoft.WindowsAzure.MobileServices;

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

            var mobileClient = new MobileServiceClient(ConnectionString.BackendUri);
            Mvx.IoCProvider.RegisterSingleton(mobileClient);

            Mvx.IoCProvider.RegisterType(typeof(ICloudService), () => new AzureCloudService(mobileClient));
            //Mvx.IoCProvider.RegisterType<ICloudService, AzureCloudService>();
        }
    }
}
