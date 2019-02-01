using System;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace GeoStat.Common.ViewModels
{
    public class RegisterViewModel : MvxViewModel
    {
        private IMvxNavigationService _navigationService;
        private IMvxLog _log;

        public RegisterViewModel(IMvxNavigationService navigationService,
            IMvxLog log)
        {
            _navigationService = navigationService;
            _log = log;
        }

        public string Email { get; set; }
        public string Password { get; set; }

        public IMvxCommand RegisterCommand => new MvxCommand(Register);

        private void Register()
        {
            _navigationService.Navigate<HomeViewModel>();
        }

    }
}
