using System;
using Android.App;
using Android.Runtime;
using Plugin.CurrentActivity;

namespace GeoStat.Droid
{
    [Application]
    public class GeoStatApplication : Application
    {
        public GeoStatApplication(IntPtr handle, JniHandleOwnership transer)
          : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            CrossCurrentActivity.Current.Init(this);
        }
    }
}
