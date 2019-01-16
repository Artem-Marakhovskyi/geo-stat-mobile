using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using GeoStat.Common.ViewModels;
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

            //StartService(5000);
        }

       private void StartService(int period)
        {
            var u = Mvx.IoCProvider.Resolve<ILocationService>();
            u.StartLocationService(period);
        }
        
    }
}
