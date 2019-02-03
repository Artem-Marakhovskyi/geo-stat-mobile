
using Android.App;
using Android.OS;
using GeoStat.Common.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;

namespace GeoStat.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(MainLauncher = true)]
    public class StartView : MvxActivity<StartViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.StartView);
        }
    }
}
