using System;
using GeoStat.Common.Services;
using GeoStat.Common.Abstractions;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

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

        private bool _isRepeatedPasswordNotValid;
        public bool IsRepeatedPasswordNotValid
        {
            get
            {
                return _isRepeatedPasswordNotValid;
            }
            set
            {
                _isRepeatedPasswordNotValid = value;
                RaisePropertyChanged();
            }
        }

        public IMvxCommand RegisterCommand => new MvxCommand(Register);

        private void Register()
        {
            IsPasswordNotValid = false;
            IsEmailNotValid = false;
            IsRepeatedPasswordNotValid = false;

            if (!_validationService.IsEmailValid(Email))
            {
                IsEmailNotValid = true;
                EmailValidationMessage = "Email is not valid";
            }

            if (Password != RepeatedPassword)
            {
                IsRepeatedPasswordNotValid = true;
                PasswordEqualityMessage = "Passwords are different!";
            }

            if (!_validationService.IsPasswordValid(Password))
            {
                IsPasswordNotValid = true;
                PasswordValidationMessage = "Password is not valid";
            }

            if (!IsPasswordNotValid && !IsEmailNotValid && !IsRepeatedPasswordNotValid)
            {
                _navigationService.Navigate<HomeViewModel>();
            }
        }
    }
}
