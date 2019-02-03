using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using GeoStat.Common.Services;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Permissions.Abstractions;

namespace GeoStat.Common.ViewModels
{
    public class StartViewModel : MvxViewModel
    {
        private readonly IUserDialogs _dialogs;
        private readonly IPermissionService _permissions;
        private readonly IMvxNavigationService _navigationService;

        private bool _isPermissionGranted;
        private bool _isPermissionAsked;

        public StartViewModel(
            IMvxNavigationService navigationService,
            IPermissionService permissions,
            IUserDialogs dialogs)
        {
            _dialogs = dialogs;
            _permissions = permissions;
            _navigationService = navigationService;
        }

        public string Explanation => AppResources.PermissionExplanation;

        public async override void ViewAppeared()
        {
            base.ViewAppeared();

            _isPermissionGranted
                = await _permissions.CheckPermissionStatusAsync(Permission.Location) == PermissionStatus.Granted;

            if (_isPermissionGranted)
            {
                await _navigationService.Navigate<LoginViewModel>();
            }

            if (!_isPermissionAsked && !_isPermissionGranted)
            {
                _isPermissionAsked = true;
                if (await SuggestLocationPermissionsAsync())
                {
                    await _navigationService.Navigate<LoginViewModel>();
                }
            }
        }

        private async Task<bool> SuggestLocationPermissionsAsync()
        {
            if ((await _permissions.RequestPermissionAsync(Permission.Location)) != PermissionStatus.Granted)
            {
                _dialogs.Alert(
                    AppResources.Permissions,
                    AppResources.Warning,
                    AppResources.Ok);

                return false;
            }

            return true;
        }
    }
}
