using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.App.Job;

using MvvmCross;
using MvvmCross.Plugin.Location;

namespace GeoStat.Droid.Services
{
    [Service(Name = "com.xamarin.samples.downloadscheduler.DownloadJob", 
         Permission = "android.permission.BIND_JOB_SERVICE")]
    public class LocationJob : JobService
    {
        private IMvxLocationWatcher _watcher;

        public async Task SaveLocationAsync(MvxGeoLocation location, string filename = null)
        {
            var fileName = "locations.txt";
            var path = System.IO.Path.Combine(
                System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal),
               fileName);

            var s = $"{DateTime.Now} {location.Coordinates.Latitude} {location.Coordinates.Longitude} ";

            using (var writer = new StreamWriter(path, true))
            {
                await writer.WriteLineAsync(s);
            }
        }

        public override bool OnStartJob(JobParameters jobParams)
        {
            _watcher = Mvx.IoCProvider.Resolve<IMvxLocationWatcher>();
            Task.Run(() =>
            {
                // Work is happening asynchronously 
                SaveLocationAsync(_watcher.CurrentLocation).Wait();          
                // Have to tell the JobScheduler the work is done. 
                JobFinished(jobParams, false);
            });
            // Return true because of the asynchronous work
            return true;
        }

        public override bool OnStopJob(JobParameters jobParams)
        {
            // we don't want to reschedule the job if it is stopped or cancelled.
            return false;
        }
    }

}