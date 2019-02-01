using System;
using Android.Runtime;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.ViewModels;
using Plugin.Permissions;

namespace GeoStat.Droid.Views
{
    public class BaseView<T> : MvxActivity<T> where T : class, IMvxViewModel
    {
        public override void OnRequestPermissionsResult(
            int requestCode, 
            string[] permissions, 
            [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}
