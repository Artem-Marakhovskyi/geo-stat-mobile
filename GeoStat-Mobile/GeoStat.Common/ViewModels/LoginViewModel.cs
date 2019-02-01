using System;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace GeoStat.Common.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private IMvxNavigationService _navigationService;
        private IMvxLog _log;

        public LoginViewModel(
            IMvxNavigationService navigationService,
            IMvxLog log)
        {
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

        private void Login()
        {
            if (Email != null && Password != null)
            {
                _navigationService.Navigate<HomeViewModel>();
            }
        }
    }
}
