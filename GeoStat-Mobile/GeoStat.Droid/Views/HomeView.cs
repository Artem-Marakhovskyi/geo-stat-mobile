using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using GeoStat.Common.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Android.Support.V4.App;
using Android.Support.Design.Widget;
using MvvmCross.Platforms.Android.Views;
using Android.Content.PM;


namespace GeoStat.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "View for HomeViewModel")]
    public class HomeView : MvxActivity<HomeViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.HomeView);
        }
    }
}