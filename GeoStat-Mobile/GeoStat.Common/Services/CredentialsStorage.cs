using System;
using GeoStat.Common.Abstractions;
using GeoStat.Common.Models;
using Plugin.SecureStorage;

namespace GeoStat.Common.Services
{
    public class CredentialsStorage : ICredentialsStorage
    {
        private readonly ISecureStorageService _secureStorage;
        private const string UserId = "UserId";
        private const string Email = "UserEmail";
        private const string Token = "Token";

        public CredentialsStorage(ISecureStorageService secureStorage)
        {
            _secureStorage = secureStorage;
        }

        public void StoreCredentials(
            AuthModel model)
        {
            _secureStorage.StoreData(UserId, model.UserId);
            _secureStorage.StoreData(Email, model.UserEmail);
            _secureStorage.StoreData(Token, model.Token);
        }

        public string GetUserId()
        {
            return _secureStorage.GetValueByKey(UserId);
        }

        public string GetUserEmail()
        {
            return _secureStorage.GetValueByKey(Email);
        }

        public string GetToken()
        {
            return _secureStorage.GetValueByKey(Token);
        }
    }
}
