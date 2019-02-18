using System;
using GeoStat.Common.Abstractions;
using GeoStat.Common.Models;
using Plugin.SecureStorage;

namespace GeoStat.Common.Services
{
    public class CredentialsStorage : ICredentialsStorage
    {
        private readonly ISecureStorageService _secureStorage;
        private const string _userId = "UserId";
        private const string _email = "UserEmail";
        private const string _token = "Token";

        public CredentialsStorage(ISecureStorageService secureStorage)
        {
            _secureStorage = secureStorage;
        }

        public void StoreCredentials(
            AuthModel model)
        {
            _secureStorage.StoreData(_userId, model.UserId);
            _secureStorage.StoreData(_email, model.UserEmail);
            _secureStorage.StoreData(_token, model.Token);
        }

        public string GetUserId()
        {
            return _secureStorage.GetValueByKey(_userId);
        }

        public string GetUserEmail()
        {
            return _secureStorage.GetValueByKey(_email);
        }

        public string GetToken()
        {
            return _secureStorage.GetValueByKey(_token);
        }
    }
}
