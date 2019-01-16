﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Views;

using MvvmCross;
using MvvmCross.Plugin;
//using GeoStat.Droid.LocationPlugin;

using GeoStat.Common.Services;
using GeoStat.Droid.Services;

namespace GeoStat.Droid
{
    [Activity(
        Label = "GeoStat.Droid"
        , MainLauncher = true
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity<MvxAndroidSetup<Common.App>, Common.App>
    {  
        public SplashScreen()
                : base(Resource.Layout.SplashScreen)
        {
            //Mvx.IoCProvider.RegisterSingleton<ILocationService>(new LocationService());
        }
    }
}