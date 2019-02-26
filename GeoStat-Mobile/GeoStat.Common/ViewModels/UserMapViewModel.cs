using System;
using System.Collections.Generic;
using System.Text;
using GeoStat.Common.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using MvvmCross.Plugin.Location;
using GeoStat.Common.Services;
using MvvmCross.Logging;
using GeoStat.Common.Abstractions;

namespace GeoStat.Common.ViewModels
{
    public class UserMapViewModel : MvxViewModel<string>
    {
        private readonly ILocationService _locationService;
        private readonly IMvxLog _log;

        public IEnumerable<LocationModel> Locations { get; set; }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public UserMapViewModel(ILocationService locationService,
                                IMvxLog log)
        {
            Title = "User Map";
            _log = log;
            _locationService = locationService;
        }

        public async override void Prepare(string userId)
        {
            Locations = await _locationService.GetLocationsByUserIdAsync(userId);
        }
    }
}
