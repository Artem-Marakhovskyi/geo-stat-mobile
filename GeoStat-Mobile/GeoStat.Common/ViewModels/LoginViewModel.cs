using System;
using Acr.UserDialogs;
using GeoStatMobile.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Permissions.Abstractions;

namespace GeoStat.Common.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private readonly IUserDialogs _dialogs;
        private readonly IPermissionService _permissions;
        private IMvxNavigationService _navigationService;
        private IMvxLog _log;

        public LoginViewModel(
            IMvxNavigationService navigationService,
            IPermissionService permissions,
            IUserDialogs dialogs,
            IMvxLog log)
        {
            _dialogs = dialogs;
            _permissions = permissions;
            _navigationService = navigationService;
            _log = log;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string EmailValidationMessage { get; set; }
        public string PasswordValidationMessage { get; set; }

        public IMvxCommand RegisterCommand => new MvxCommand(Register);

        private void Register()
        {
            _navigationService.Navigate<RegisterViewModel>();
        }

        public IMvxCommand LoginCommand => new MvxCommand(Login);

        private async void Login()
        {
            if (Email != null && Password != null)
            {
                if ((await _permissions.RequestPermissionAsync(Permission.Location)) != PermissionStatus.Granted)
                {
                    _dialogs.Alert(
                        "Without prompted permission you can not login to application. Allow it in settings",
                        "Warning",
                        "Ok");
                }
                else
                {
                    _navigationService.Navigate<HomeViewModel>();
                }
            }
        }
    }
}
