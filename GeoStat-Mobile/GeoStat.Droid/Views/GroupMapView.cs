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
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using MvvmCross.Droid.Support.V4;
using GeoStat.Common.ViewModels;



namespace GeoStat.Droid.Views
{
    [Activity(Label = "View for GroupMapViewModel")]
    public class GroupMapView : MvxFragmentActivity<GroupMapViewModel>, IOnMapReadyCallback
    {
        private BitmapDescriptor[] _descriptors;
        private Dictionary<string, BitmapDescriptor> groupDescriptors;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MapView);
            
            var mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);
        }

        public void OnMapReady(GoogleMap map)
        {
            _descriptors = new BitmapDescriptor[]
        {
            BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueRed),
            BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueAzure),
            BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueGreen),
            BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueYellow),
            BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueCyan),
            BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueMagenta),
            BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueOrange),
            BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueBlue),
            BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueViolet),
            BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueRose)
        };
            // Drow markers on map
            var members = ViewModel.GroupMembers.ToArray();
            groupDescriptors = new Dictionary<string, BitmapDescriptor>();
            for (var i = 0; i < members.Count(); i++)
            {
                groupDescriptors.Add(members[i].UserId, _descriptors[i % _descriptors.Count()]);
            }

            var markers = new List<MarkerOptions>();

            foreach (var location in ViewModel.GroupLocations)
            {
                var desc = BitmapDescriptorFactory.DefaultMarker();
                groupDescriptors.TryGetValue(location.UserId, out desc);

                markers.Add(new MarkerOptions()
                    .SetPosition(new LatLng(location.Latitude, location.Longitude))
                    .SetTitle($"{location.UserId}").SetIcon(desc));
            }

            foreach (var m in markers)
            {
                map.AddMarker(m);
            }
        }
    }
}