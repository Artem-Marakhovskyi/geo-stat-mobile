using Android.App;
using Android.OS;
using GeoStat.Common.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace GeoStat.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "View for HomeViewModel")]
    public class HomeView : BaseView<HomeViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.HomeView);
        }
    }
}