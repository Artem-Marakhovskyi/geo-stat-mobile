﻿using MvvmCross.IoC;
using MvvmCross.ViewModels;
using GeoStat.Common.ViewModels;
using GeoStat_Mobile.Abstractions;
using GeoStat_Mobile.Services;

namespace GeoStat.Common
{
    public class App : MvxApplication
    {
        public static ICloudService CloudService { get; set; }

        public override void Initialize()
        {
            CloudService = new AzureCloudService();

            //MainPage = new NavigationPage(new Pages.EntryPage());
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            RegisterAppStart<HomeViewModel>();
        }
    }
}