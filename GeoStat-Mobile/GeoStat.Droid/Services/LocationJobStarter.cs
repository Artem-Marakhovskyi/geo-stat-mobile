using System;
using Android.App;
using Android.Content;
using Android.App.Job;
using GeoStat.Common.Locations;
using System.Threading.Tasks;

namespace GeoStat.Droid.Services
{
    public class LocationJobStarter : ILocationJobStarter
    {
        public void StartLocationJob(int periodMs)
        {
            new LocationFileManager().CreateFileIfNotExists();
            var jobBuilder = Application.Context.CreateJobBuilderUsingJobId<LocationJob>(jobId:1);
            var job = jobBuilder.SetPeriodic(periodMs).SetPersisted(true).Build();

            var jobScheduler 
                = (JobScheduler)Application.Context.
                    GetSystemService(Context.JobSchedulerService);

            var result = jobScheduler.Schedule(job);

            if (result == 0)
            {
                throw new InvalidOperationException("Location job cannot be started.");
            }
        }       
    }
}