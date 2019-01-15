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

using MvvmCross.Plugin.Location;

namespace GeoStat.Droid.Services
{
    [Service(Name = "com.xamarin.samples.downloadscheduler.DownloadJob", 
         Permission = "android.permission.BIND_JOB_SERVICE")]
    public class LocationJob : JobService
    {
        private static int count;

        public async Task SaveCountAsync(int count)
        {
            //Android.Content.Context.GetExternalFilesDir(string type)
            var backingFile = Path.Combine(Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments).ToString(), "count.txt");
            using (var writer = File.CreateText(backingFile))
            {
                await writer.WriteLineAsync(count.ToString());
            }
        }

        public override bool OnStartJob(JobParameters jobParams)
        {
            Task.Run(() =>
            {
                // Work is happening asynchronously

                // Get location and save it 
                count++;
                SaveCountAsync(count);


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