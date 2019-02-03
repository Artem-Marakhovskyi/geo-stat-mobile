using System;
using GeoStat.Common.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Acr.UserDialogs;
using GeoStatMobile.Services;
using Plugin.Permissions.Abstractions;
using GeoStat_Mobile;
using System.Threading.Tasks;

namespace GeoStat.Common.ViewModels
{
    public class RegisterViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IValidationService _validationService;
        private readonly IMvxLog _log;
        private readonly IUserDialogs _dialogs;
        private readonly IPermissionService _permissions;

        public RegisterViewModel(
            IMvxNavigationService navigationService,
            IValidationService validationService,
            IPermissionService permissions,
            IUserDialogs dialogs,
            IMvxLog log)
        {
            _navigationService = navigationService;
            _validationService = validationService;
            _permissions = permissions;
            _dialogs = dialogs;
            _log = log;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatedPassword { get; set; }

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

        private string _passwordEqualityMessage;
        public string PasswordEqualityMessage
        {
            get
            {
                return _passwordEqualityMessage;
            }
            set
            {
                _passwordEqualityMessage = value;
                RaisePropertyChanged();
            }
        }

        private bool _isEmailValid;
        public bool IsEmailValid
        {
            get
            {
                return _isEmailValid;
            }
            set
            {
                _isEmailValid = value;
                RaisePropertyChanged();
            }
        }

        private bool _isPasswordValid;
        public bool IsPasswordValid
        {
            get
            {
                return _isPasswordValid;
            }
            set
            {
                _isPasswordValid = value;
                RaisePropertyChanged();
            }
        }

        private bool _isRepeatedPasswordValid;
        public bool IsRepeatedPasswordValid
        {
            get
            {
                return _isRepeatedPasswordValid;
            }
            set
            {
                _isRepeatedPasswordValid = value;
                RaisePropertyChanged();
            }
        }

        public IMvxCommand RegisterCommand => new MvxCommand(Register);

        private async void Register()
        {
            IsEmailValid = _validationService.IsEmailValid(Email);
            IsRepeatedPasswordValid = Password == RepeatedPassword;
            IsPasswordValid = _validationService.IsPasswordValid(Password);

            if (!IsEmailValid)
            {
                EmailValidationMessage = AppResources.EmailInvalid;
            }

            if (IsRepeatedPasswordValid)
            {
                PasswordEqualityMessage = AppResources.RepeatedPasswordInvalid;
            }

            if (!IsPasswordValid)
            {
                PasswordValidationMessage = AppResources.PasswordInvalid;
            }

            if (!IsEmailValid || !IsPasswordValid || !IsRepeatedPasswordValid)
            {
                return;
            }

            if (await SuggestLocationPermissionsAsync())
            {
                await _navigationService.Navigate<HomeViewModel>();
            }
        }

        private async Task<bool> SuggestLocationPermissionsAsync()
        {
            if ((await _permissions.RequestPermissionAsync(Permission.Location)) != PermissionStatus.Granted)
            {
                _dialogs.Alert(
                    AppResources.Permissions,
                    "Warning",
                    "Ok");

                return false;
            }

            return true;
        }
    }
}
