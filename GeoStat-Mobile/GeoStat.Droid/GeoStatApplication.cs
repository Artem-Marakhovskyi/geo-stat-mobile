using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Android.App;
using Android.Runtime;
using GeoStat.Common;
using GeoStat.Droid.Services;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Views;
using Plugin.CurrentActivity;

namespace GeoStat.Droid
{
    [Application]
    public class MainApplication : MvxAndroidApplication<MvxAndroidSetup<App>, App>
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            CrossCurrentActivity.Current.Init(this);
            UserDialogs.Init(this);
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            new LocationJobStarter().StartLocationJob(1000);
        }
    }
}
