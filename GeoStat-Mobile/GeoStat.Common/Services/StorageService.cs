using System;
using GeoStat.Common.Abstractions;
using GeoStat.Common.Models;
using Plugin.SecureStorage;

namespace GeoStat.Common.Services
{
    public class StorageService : IStorageService
    {
        public void StoreCredentials(AuthModel model)
        {
            CrossSecureStorage.Current.SetValue("UserId", model.UserId);
            CrossSecureStorage.Current.SetValue("UserEmail", model.UserEmail);
            CrossSecureStorage.Current.SetValue("Token", model.Token);
        }

        public string GetUserId()
        {
            if (CrossSecureStorage.Current.HasKey("UserId"))
            {
                return CrossSecureStorage.Current.GetValue("UserId");
            }

            return string.Empty;
        }

        public string GetUserEmail()
        {
            if (CrossSecureStorage.Current.HasKey("UserEmail"))
            {
                return CrossSecureStorage.Current.GetValue("UserEmail");
            }

            return string.Empty;
        }

        public string GetToken()
        {
            if (CrossSecureStorage.Current.HasKey("Token"))
            {
                return CrossSecureStorage.Current.GetValue("Token");
            }

            return string.Empty;
        }
    }
}
