using System;
using GeoStat.Common.Abstractions;
using Microsoft.WindowsAzure.MobileServices;

namespace GeoStat.Common.Services
{
    public class AzureCloudService : ICloudService
    {
        private readonly MobileServiceClient _client;
        private readonly string backendUri = "https://my-backend.azurewebsites.net";

        public AzureCloudService()
        {
            _client = new MobileServiceClient(backendUri);
        }

        public ICloudTable<T> GetTable<T>() where T : TableData
        {
            return new AzureCloudTable<T>(_client);
        }
    }
}
