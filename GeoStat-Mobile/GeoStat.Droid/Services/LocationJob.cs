using Android.App;
using Android.App.Job;
using Android.Gms.Location;
using Android.Util;

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
            System.Threading.Tasks.Task.Run(
                async () =>
                {
                    var client = LocationServices.GetFusedLocationProviderClient(this);
                    try
                    {
                        var completeListener = new LocationTaskCompleteListener(client);
                        client.LastLocation.AddOnCompleteListener(completeListener);

                        await completeListener.WaitForCompletionAsync();

                        JobFinished(jobParams, false);
                    }
                    catch (System.Exception ex)
                    {
                        Log.Error("LOCATION_JOB", ex.ToString());
                    }
                });

            return true;
        }

        public override bool OnStopJob(JobParameters jobParams)
        {
            // we don't want to reschedule the job if it is stopped or cancelled.
            return false;
        }
    }

}