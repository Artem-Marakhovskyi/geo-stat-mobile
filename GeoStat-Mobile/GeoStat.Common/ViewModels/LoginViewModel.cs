﻿using GeoStat.Common.Models;
using GeoStat.Common.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace GeoStat.Common.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IValidationService _validationService;
        private readonly IMvxLog _log;
        private readonly HttpService _httpService;

        public LoginViewModel(
            IMvxNavigationService navigationService,
            IValidationService validationService,
            IMvxLog log,
            HttpService httpService)
        {
            _navigationService = navigationService;
            _validationService = validationService;
            _log = log;
            _httpService = httpService;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string EmailValidationMessage => AppResources.EmailInvalid;
        public string PasswordValidationMessage => AppResources.PasswordInvalid;

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

        public IMvxCommand RegisterCommand => new MvxCommand(Register);

        private void Register()
        {
            _navigationService.Navigate<RegisterViewModel>();
        }

        public IMvxCommand LoginCommand => new MvxCommand(Login);

        private async void Login()
        {
            IsPasswordValid = _validationService.IsPasswordValid(Password);
            IsEmailValid = _validationService.IsEmailValid(Email);

            if (!IsEmailValid || !IsPasswordValid)
            {
                return;
            }

            var model = new LoginModel(Email, Password);

            await _httpService.SendRequestForLogin(model);

            await _navigationService.Navigate<HomeViewModel>();
        }
    }
}
