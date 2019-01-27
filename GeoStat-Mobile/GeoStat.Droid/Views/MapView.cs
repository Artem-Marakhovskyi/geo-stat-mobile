using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using GeoStat.Common.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Android.Support.V4.App;
using Android.Support.Design.Widget;
using MvvmCross.Platforms.Android.Views;
using Android.Content.PM;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using MvvmCross.Droid.Support.V4;


namespace GeoStat.Droid.Views
{
    [Activity(Label = "View for MapViewModel")]
    public class MapView : MvxFragmentActivity<MapViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MapView);
        }
    }
}