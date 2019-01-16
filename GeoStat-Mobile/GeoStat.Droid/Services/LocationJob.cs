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

        public async Task SaveLocationAsync(MvxGeoLocation location)
        {
            //Android.Content.Context.GetExternalFilesDir(string type)
            var backingFile = Path.Combine(Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments).ToString(), "locations.txt");
            using (var writer = File.CreateText(backingFile))
            {
                await writer.WriteLineAsync(location.ToString());
            }
        }

        public override bool OnStartJob(JobParameters jobParams)
        {
            _watcher = Mvx.IoCProvider.Resolve<IMvxLocationWatcher>();
            Task.Run(() =>
            {
                // Work is happening asynchronously

                // Get location and save it 


                SaveLocationAsync(_watcher.CurrentLocation);
               


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