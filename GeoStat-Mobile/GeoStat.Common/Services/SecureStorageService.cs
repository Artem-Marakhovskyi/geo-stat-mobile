using System;
using GeoStat.Common.Abstractions;
using Plugin.SecureStorage;
using Plugin.SecureStorage.Abstractions;

namespace GeoStat.Common.Services
{
    public class SecureStorageService : ISecureStorageService
    {
        private readonly ISecureStorage _secureStorage;

        public SecureStorageService(ISecureStorage secureStorage)
        {
            _secureStorage = secureStorage;
        }

        public void StoreData(string key, string value)
        {
            _secureStorage.SetValue(key, value);
        }

        public string GetValueByKey(string key)
        {
            if (_secureStorage.HasKey(key))
            {
                return _secureStorage.GetValue(key);
            }

            return null;
        }

        public void DeleteValueByKey(string key)
        {
            if (_secureStorage.HasKey(key))
            {
                _secureStorage.DeleteKey(key);
            }
        }
    }
}
