using MvvmCross.IoC;
using MvvmCross.ViewModels;
using GeoStat.Common.ViewModels;
using GeoStat.Common.Abstractions;
using GeoStat.Common.Services;
using MvvmCross;
using Microsoft.WindowsAzure.MobileServices;
using AutoMapper;
using GeoStat.Common.Models;
using System.Net.Http;

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

            RegisterAppStart<LoginViewModel>();

            var mobileClient = new MobileServiceClient(ConnectionString.BackendUri);
            var storageService = new StorageService();
            var user = new UserContext(storageService);

            Mvx.IoCProvider.RegisterSingleton(mobileClient);
            Mvx.IoCProvider.RegisterSingleton(user);
            Mvx.IoCProvider.RegisterSingleton(storageService);

            var config = CreateMapperConfig();

            Mvx.IoCProvider.RegisterType(
                typeof(IMapper),
                () => config.CreateMapper());
            Mvx.IoCProvider.RegisterType(
                typeof(ICloudService),
                () => new AzureCloudService(mobileClient));
            Mvx.IoCProvider.RegisterType(
                typeof(IGeoStatRepository<>),
                typeof(GeoStatRepository<>));
            Mvx.IoCProvider.RegisterType<GroupService>();
            Mvx.IoCProvider.RegisterType<LocationService>();
            Mvx.IoCProvider.RegisterType<UserService>();
            Mvx.IoCProvider.RegisterType<LoggingHandler>();
            Mvx.IoCProvider.RegisterType(
             () => new HttpClient(Mvx.IoCProvider.Resolve<LoggingHandler>()));
            Mvx.IoCProvider.RegisterType<HttpService>();
        }

        private MapperConfiguration CreateMapperConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LocationModel, Location>();
                cfg.CreateMap<GroupModel, Group>();
                cfg.CreateMap<UserModel, GeoStatUser>();
            });

            return config;
        }
    }
}
