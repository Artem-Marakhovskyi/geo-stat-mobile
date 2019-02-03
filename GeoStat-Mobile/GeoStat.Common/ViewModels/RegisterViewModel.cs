using GeoStat.Common.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Acr.UserDialogs;

namespace GeoStat.Common.ViewModels
{
    public class RegisterViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IValidationService _validationService;
        private readonly IMvxLog _log;

        public RegisterViewModel(
            IMvxNavigationService navigationService,
            IValidationService validationService,
            IUserDialogs dialogs,
            IMvxLog log)
        {
            _navigationService = navigationService;
            _validationService = validationService;
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

            await _navigationService.Navigate<HomeViewModel>();

        }
    }
}
