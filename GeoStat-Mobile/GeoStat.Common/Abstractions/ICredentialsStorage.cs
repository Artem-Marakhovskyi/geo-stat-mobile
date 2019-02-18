using System;
using GeoStat.Common.Models;

namespace GeoStat.Common.Abstractions
{
    public interface ICredentialsStorage
    {
        void StoreCredentials(AuthModel model);

        string GetUserId();
        string GetUserEmail();
        string GetToken();
    }
}
