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
using Android.App.Job;
using Xamarin.Android;
using GeoStat.Common.Services;
using MvvmCross.Plugin.Location;
using System.Threading.Tasks;
using System.IO;


namespace GeoStat.Droid.Services
{
    class GeoLocationService : ILocationService 
    {
        public IEnumerable<string> GetLocations()
        {
            var fileName = "locations.txt";
            var path = System.IO.Path.Combine(
                System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal),
               fileName);
           
            var locations = ReadLocations(path);
            return locations;
        }

        public IEnumerable<string> ReadLocations(string path)
        {
            if (path == null || !File.Exists(path))
            {
                string[] err = { "File not found" };
                return err;

            }
            
            var locations = File.ReadAllLines(path);
            return locations;
        }

        public void StartLocationService(int period)
        {
            var jobBuilder = Application.Context.CreateJobBuilderUsingJobId<LocationJob>(1);
            jobBuilder.SetPeriodic(period);
            //jobBuilder.SetPersisted(true);
            var jobInfo = jobBuilder.Build();

            var jobScheduler = (JobScheduler)Application.Context.
                GetSystemService(Android.Content.Context.JobSchedulerService);
            var scheduleResult = jobScheduler.Schedule(jobInfo);
        }       
    }
}