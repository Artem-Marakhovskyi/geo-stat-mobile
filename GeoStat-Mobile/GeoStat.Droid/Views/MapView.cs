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
using GeoStat.Common.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Android.Support.V4.App;
using Android.Support.Design.Widget;
using MvvmCross.Platforms.Android.Views;
using Android.Content.PM;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using MvvmCross.Droid.Support.V4;


namespace GeoStat.Droid.Views
{
    [Activity(Label = "View for MapViewModel")]
    public class MapView : MvxFragmentActivity<MapViewModel>, IOnMapReadyCallback
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
            var currentLocation = new LatLng(ViewModel.Lat, ViewModel.Lng);

            var builder = CameraPosition.InvokeBuilder();
            builder.Target(currentLocation);
            builder.Zoom(15);

            CameraPosition cameraPosition = builder.Build();
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            map.MoveCamera(cameraUpdate);

            var currentLocationMarkerOpt = new MarkerOptions();
            currentLocationMarkerOpt.SetPosition(currentLocation);
            currentLocationMarkerOpt.SetTitle("I'm here!");

            var currentLocationBmDescriptor = BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueRed);
            currentLocationMarkerOpt.SetIcon(currentLocationBmDescriptor);

            map.AddMarker(currentLocationMarkerOpt);

            var locationsBmDescriptor = BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueAzure);

            var markers = new List<MarkerOptions>();

            foreach (var location in ViewModel.Locations)
            {
                markers.Add(new MarkerOptions()
                    .SetPosition(new LatLng(location.Latitude, location.Longitude))
                    .SetTitle($"{location.Latitude} {location.Longitude}").SetIcon(locationsBmDescriptor));
            }

            foreach (var m in markers)
            {
                map.AddMarker(m);
            }
        }
    }
}