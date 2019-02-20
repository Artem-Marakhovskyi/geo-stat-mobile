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
        private readonly IMvxLog _log;


        public IEnumerable<GroupModel> Groups { get; set; }
        private readonly ILocationService _locationService;

        private readonly ICloudService _cloudService;

        public HomeViewModel(
            IMvxNavigationService navigationService,
            ILocationJobStarter locationJobStarter,
            ILocationFileManager locationFileManager,
            IMvxLog log,
            UserService userService,
            ILocationService locationService,
            ICloudService cloudService)
        {
            _locationFileManager = locationFileManager;
            _navigationService = navigationService;
            _locationJobStarter = locationJobStarter;
            _log = log;
            _userService = userService;
            _locationService = locationService;
            _cloudService = cloudService;
        }

        public async override void Start()
        {
            base.Start();

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
            LocationsCount = locations.Count();
            LatestLocation = "empty";

            await _cloudService.SyncOfflineCacheAsync();
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

    }
}
