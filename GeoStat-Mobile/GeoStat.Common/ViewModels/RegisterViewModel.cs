using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using GeoStat.Common.Abstractions;

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
            IMvxLog log)
        {
            _navigationService = navigationService;
            _validationService = validationService;
            _log = log;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatedPassword { get; set; }

        public string EmailValidationMessage => AppResources.EmailInvalid;
        public string PasswordValidationMessage => AppResources.PasswordInvalid;
        public string PasswordEqualityMessage => AppResources.RepeatedPasswordInvalid;

        private bool _isEmailValid = true;
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

        private bool _isPasswordValid = true;
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

        private bool _isRepeatedPasswordValid = true;
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

            if (!IsEmailValid || !IsPasswordValid || !IsRepeatedPasswordValid)
            {
                return;
            }

            await _navigationService.Navigate<HomeViewModel>();

        }
    }
}
