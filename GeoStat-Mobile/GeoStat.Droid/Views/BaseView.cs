using System;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.ViewModels;

namespace GeoStat.Droid.Views
{
    public class BaseView<T> : MvxActivity<T> where T : class, IMvxViewModel
    {
    }
}
