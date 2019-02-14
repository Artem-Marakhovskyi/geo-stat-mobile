using System.Collections.Generic;
using MvvmCross.Commands;
using System.Windows.Input;
using MvvmCross.ViewModels;
using MvvmCross.Plugin.Location;
using System.Linq;
using MvvmCross.Navigation;
using MvvmCross.Logging;
using GeoStat.Common.Models;
using GeoStat.Common.Locations;
using GeoStat.Common.Services;
using GeoStat.Common.Abstractions;

namespace GeoStat.Common.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        private readonly ILocationFileManager _locationFileManager;
        private readonly IMvxNavigationService _navigationService;
        private readonly ILocationJobStarter _locationJobStarter;
        private readonly UserService _userService;
        private readonly LocationService _locationService;
        private readonly IMvxLog _log;
        private readonly ICloudService _cloudService;
        private readonly IMvxLocationWatcher _watcher;

        public IEnumerable<GroupModel> Groups { get; set; }

        public HomeViewModel(
            IMvxNavigationService navigationService, 
            ILocationJobStarter locationJobStarter,
            ILocationFileManager locationFileManager,
            IMvxLog log,
            UserService userService,
            LocationService locationService,
            ICloudService cloudService, 
            IMvxLocationWatcher watcher)
        {
            _locationFileManager = locationFileManager;
            _navigationService = navigationService;
            _locationJobStarter = locationJobStarter;
            _log = log;
            _userService = userService;
            _locationService = locationService;
            _cloudService = cloudService;
            _watcher = watcher;
        }

        public async override void Start()
        {
            base.Start();
            _watcher.Start(new MvxLocationOptions(), OnLocation, OnError);

            var locations = _locationFileManager.ReadLocations();
            
            _locationFileManager.RemoveFile();
            _locationJobStarter.StartLocationJob(16 * 60 * 1000);

            //Groups = await _userService.GetGroupsOfUser();
            Groups = new List<GroupModel>
            {
                new GroupModel ("group1", "first group", "user1"),
                new GroupModel ("group2", "second group", "user1"),
                new GroupModel ("group3", "third group", "user1")
            };
        }
        
        private void ShowUserMap()
        {
            _navigationService.Navigate<UserMapViewModel>();
        }

        public void ShowGroupMapById(GroupModel group)
        {
            _navigationService.Navigate<GroupViewModel, GroupModel>(group);
        }

        public IMvxCommand ShowGroupByIdCommand => new MvxCommand<GroupModel>((group) => ShowGroupMapById(group));
        public IMvxCommand ShowUserMapCommand => new MvxCommand(ShowUserMap);

        public void OnLocation(MvxGeoLocation location) { }

        public void OnError(MvxLocationError error)
        {
            _log.Error(error.Code.ToString());
        }
    }
}
