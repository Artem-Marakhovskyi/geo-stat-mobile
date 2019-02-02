using System.Threading.Tasks;
using Android.App;
using Android.App.Job;
using Android.Gms.Location;
using Android.Gms.Tasks;
using GeoStat.Common.Locations;
using Java.Lang;
using MvvmCross.Plugin.Location;

namespace GeoStat.Droid.Services
{
    [Service(
        Name = "geostat.droid.LocationJob", 
        Enabled = true,
        Permission = "android.permission.BIND_JOB_SERVICE")]
    public class LocationJob : JobService
    {
        public override bool OnStartJob(JobParameters jobParams)
        {
            var watcher = new MvvmCross.Plugin.Location.Fused.MvxAndroidFusedLocationWatcher();

            watcher.Start(
                new MvxLocationOptions(),
                location =>
                {
                    new LocationFileManager().AddLine("2");
                    var locationFileManager = new LocationFileManager();
                    locationFileManager.AddLocation(location);

                    new LocationFileManager().AddLine("");
                    JobFinished(jobParams, false);
                },
                e => { /* Error happened, no need to be logged in some way */ });
            new LocationFileManager().AddLine("1");
            return true;
        }

        public override bool OnStopJob(JobParameters jobParams)
        {
            // we don't want to reschedule the job if it is stopped or cancelled.
            return false;
        }
    }

}