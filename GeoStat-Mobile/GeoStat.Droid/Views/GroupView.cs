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

namespace GeoStat.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "View for MapViewModel")]
    public class GroupView : BaseView<GroupViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.GroupView);
        }
    }
}