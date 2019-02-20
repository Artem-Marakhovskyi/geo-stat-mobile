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
        private readonly IUserService _userService;
        private readonly UserContext _userContext;
        private readonly IMvxLog _log;

        private IEnumerable<GroupModel> _groups;
        public IEnumerable<GroupModel> Groups
        {
            get { return _groups; }
            set { _groups = value; RaisePropertyChanged(() => Groups); }
        }

        private readonly ILocationService _locationService;

        private readonly ICloudService _cloudService;

        public HomeViewModel(
            IMvxNavigationService navigationService,
            ILocationJobStarter locationJobStarter,
            ILocationFileManager locationFileManager,
            IMvxLog log,
            IUserService userService,
            ILocationService locationService,
            UserContext userContext, 
            ICloudService cloudService)
        {
            _userContext = userContext;
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

            Groups = await _userService.GetGroupsOfUser();
            if (Groups is null)
            {
                Groups = new List<GroupModel>();
            }

            await _cloudService.SyncOfflineCacheAsync();
        }
        
        private void ShowUserMap()
        {
            _navigationService.Navigate<UserMapViewModel, string>(_userContext.UserId);
        }

        public void ShowGroupMapById(GroupModel group)
        {
            _navigationService.Navigate<GroupViewModel, GroupModel>(group);
        }

        public IMvxCommand ShowGroupByIdCommand => new MvxCommand<GroupModel>((group) => ShowGroupMapById(group));
        public IMvxCommand ShowUserMapCommand => new MvxCommand(ShowUserMap);

    }
}
