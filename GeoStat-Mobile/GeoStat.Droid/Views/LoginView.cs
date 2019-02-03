using Android.App;
using Android.OS;
using GeoStat.Common.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;

namespace GeoStat.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "View for LoginViewModel", MainLauncher = true)]
    public class LoginView : MvxActivity<LoginViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LoginView);
        }
    }
}
