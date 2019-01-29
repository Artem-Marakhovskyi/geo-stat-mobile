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
            LatLng location = new LatLng(ViewModel.Lat, ViewModel.Lng);

            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(location);
            builder.Zoom(10);

            CameraPosition cameraPosition = builder.Build();
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            map.MoveCamera(cameraUpdate);

            MarkerOptions markerOpt1 = new MarkerOptions();
            markerOpt1.SetPosition(location);
            markerOpt1.SetTitle("I'm here!");

            var bmDescriptor1 = BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueRed);
            markerOpt1.SetIcon(bmDescriptor1);

            map.AddMarker(markerOpt1);

            var bmDescriptor2 = BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueAzure);

            List<MarkerOptions> markers = new List<MarkerOptions>();

            foreach (var l in ViewModel.Locations)
            {
                markers.Add(new MarkerOptions().
                    SetPosition(new LatLng(l.Latitude, l.Longitude))
                    .SetTitle($"{l.Latitude} {l.Longitude}").SetIcon(bmDescriptor2));
            }

            foreach (var m in markers)
            {
                map.AddMarker(m);
            }

        }
    }

}