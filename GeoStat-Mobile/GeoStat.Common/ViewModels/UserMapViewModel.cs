﻿using System;
using System.Collections.Generic;
using System.Text;
using GeoStat.Common.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using MvvmCross.Plugin.Location;
using GeoStat.Common.Services;
using MvvmCross.Logging;

namespace GeoStat.Common.ViewModels
{
    public class UserMapViewModel : MvxViewModel
    {
        private readonly IMvxLocationWatcher _watcher;
        private readonly LocationService _locationService;
        private readonly IMvxLog _log;
        public IEnumerable<LocationModel> Locations { get; set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(() => Title); }
        }

        public UserMapViewModel(LocationService locationService,
                                IMvxLocationWatcher watcher,
                                IMvxLog log)
        {
            Title = "User Map";
            _log = log;
            _locationService = locationService;
            _watcher = watcher;

            Lng = _watcher.CurrentLocation.Coordinates.Longitude;
            Lat = _watcher.CurrentLocation.Coordinates.Latitude;
        }

        public async override void Start()
        {
            base.Start();
            //Locations = await _locationService.GetLocationsOfUserAsync();
            //Locations = new List<LocationModel>();
            Locations = new List<LocationModel>
            {
                new LocationModel( 7.3, 67.2, DateTimeOffset.Now),
                new LocationModel (0.3, 0.2, DateTimeOffset.Now),
                new LocationModel (12.4, 12.5, DateTimeOffset.Now),
                new LocationModel (34.7, 17.9, DateTimeOffset.Now),
                new LocationModel (67.9, 14.6, DateTimeOffset.Now),
                new LocationModel (12.4,  62.5, DateTimeOffset.Now),
                new LocationModel (7.6, 26.4, DateTimeOffset.Now),
                new LocationModel (12.8, 36.6, DateTimeOffset.Now),
                new LocationModel (44.7, 37.1, DateTimeOffset.Now),
                new LocationModel (76.9, 56.9, DateTimeOffset.Now),
                new LocationModel (14.7, 12.6, DateTimeOffset.Now),
                new LocationModel (16.9, 67.3, DateTimeOffset.Now),
                new LocationModel (46.8, 35.8, DateTimeOffset.Now),
                new LocationModel (32.5, 32.5, DateTimeOffset.Now),
                new LocationModel (23.3, 81.4, DateTimeOffset.Now),
                new LocationModel (4.6, 80.9, DateTimeOffset.Now)
            };
        }
        
        private double _lng;
        public double Lng
        {
            get { return _lng; }
            set { _lng = value; RaisePropertyChanged(() => Lng); }
        }

        private double _lat;
        public double Lat
        {
            get { return _lat; }
            set { _lat = value; RaisePropertyChanged(() => Lat); }
        }
    }
}