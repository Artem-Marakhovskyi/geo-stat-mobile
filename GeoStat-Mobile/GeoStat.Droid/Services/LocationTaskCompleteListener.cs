using System;
using System.Threading.Tasks;
using Android.Gms.Location;
using Android.Gms.Tasks;
using Android.Util;
using GeoStat.Common.Locations;

namespace GeoStat.Droid.Services
{
    public class LocationTaskCompleteListener : Java.Lang.Object, IOnCompleteListener
    {
        private const string Tag = "LOCATION_JOB";

        private readonly FusedLocationProviderClient _client;

        private System.Threading.Tasks.TaskCompletionSource<bool> _tcs;

        public LocationTaskCompleteListener(
            FusedLocationProviderClient client)
        {
            _client = client;
            _tcs = new TaskCompletionSource<bool>();
        }

        public Task<bool> WaitForCompletionAsync() => _tcs.Task;

        public async void OnComplete(Android.Gms.Tasks.Task task)
        {
            var location = await _client.GetLastLocationAsync();
            var locationFileManager = new LocationFileManager();
            Log.Info(Tag, $"{location.Latitude}, {location.Longitude}");
            locationFileManager.AddLocation(new LocationCoordinate(location.Longitude, location.Latitude));
            Log.Info(Tag, $"Location saved");

            if (!_tcs.Task.IsCompleted)
            {
                _tcs.SetResult(true);
            }
        }
    }
}
