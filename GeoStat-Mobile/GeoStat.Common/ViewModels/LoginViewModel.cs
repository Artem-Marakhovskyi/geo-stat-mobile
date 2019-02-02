using Acr.UserDialogs;
using GeoStat.Common.Locations;
using GeoStat.Common.Services;
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
        private readonly IMvxNavigationService _navigationService;
        private readonly IValidationService _validationService;
        private readonly IMvxLog _log;
        private readonly IUserDialogs _dialogs;
        private readonly IPermissionService _permissions;

        public LoginViewModel(
            IMvxNavigationService navigationService,
            IValidationService validationService,
            IPermissionService permissions,
            IUserDialogs dialogs,
            IMvxLog log)
        {
            _dialogs = dialogs;
            _permissions = permissions;
            _navigationService = navigationService;
            _validationService = validationService;
            _log = log;
        }

        public string Email { get; set; }
        public string Password { get; set; }

        private string _emailValidationMessage;
        public string EmailValidationMessage
        {
            get
            {
                return _emailValidationMessage;
            }
            set
            {
                _emailValidationMessage = value;
                RaisePropertyChanged();
            }
        }

        private string _passwordValidationMessage;
        public string PasswordValidationMessage
        {
            get
            {
                return _passwordValidationMessage;
            }
            set
            {
                _passwordValidationMessage = value;
                RaisePropertyChanged();
            }
        }

        private bool _isEmailNotValid;
        public bool IsEmailNotValid
        {
            get
            {
                return _isEmailNotValid;
            }
            set
            {
                _isEmailNotValid = value;
                RaisePropertyChanged();
            }
        }

        private bool _isPasswordNotValid;
        public bool IsPasswordNotValid
        {
            get
            {
                return _isPasswordNotValid;
            }
            set
            {
                _isPasswordNotValid = value;
                RaisePropertyChanged();
            }
        }

        public IMvxCommand RegisterCommand => new MvxCommand(Register);

        private void Register()
        {
            _navigationService.Navigate<RegisterViewModel>();
        }

        public IMvxCommand LoginCommand => new MvxCommand(Login);

        private async void Login()
        {
            IsPasswordNotValid = false;
            IsEmailNotValid = false;

            if (!_validationService.IsEmailValid(Email))
            {
                IsEmailNotValid = true;
                EmailValidationMessage = "Email is not valid";
            }

            if (!_validationService.IsPasswordValid(Password))
            {
                IsPasswordNotValid = true;
                PasswordValidationMessage = "Password is not valid";
            }

            if (!IsEmailNotValid && !IsPasswordNotValid)
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
                    await _navigationService.Navigate<HomeViewModel>();
                }
            }
        }
    }
}
