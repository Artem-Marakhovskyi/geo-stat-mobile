using System.Collections.Generic;
using Android.App;
using Android.OS;
using GeoStat.Common.ViewModels;
using Android.Support.V4.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using MvvmCross.Droid.Support.V4;
using Android.Gms.Location;

namespace GeoStat.Droid.Views
{
    [Activity(Label = "View for UserMapViewModel")]
    public class UserMapView : MvxFragmentActivity<UserMapViewModel>, IOnMapReadyCallback
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MapView);

            var mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);
        }

        public void OnMapReady(GoogleMap map)
        {
            map.MyLocationEnabled = true;
         
            var markers = new List<MarkerOptions>();

            foreach (var location in ViewModel.Locations)
            {
                markers.Add(new MarkerOptions()
                    .SetPosition(new LatLng(location.Latitude, location.Longitude))
                    .SetTitle($"{location.Latitude} {location.Longitude}"));
            }

            foreach (var m in markers)
            {
                map.AddMarker(m);
            }
        }
    }
}