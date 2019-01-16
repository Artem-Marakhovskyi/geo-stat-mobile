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


namespace GeoStat.Droid.Services
{
    class LocationService : ILocationService 
    {  

        public MvxGeoLocation GetCurrentLocation(IMvxLocationWatcher watcher)
        {
            return watcher.CurrentLocation;
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