using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GeoStat.Common.Abstractions;
using GeoStat.Common.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Plugin.Connectivity;
using MvvmCross.Logging;

namespace GeoStat.Common.Services
{
    public class AzureCloudService : ICloudService
    {
        private readonly MobileServiceClient _client;

        public AzureCloudService (MobileServiceClient mobileServiceClient)
        {
            _client = mobileServiceClient;
        }

        public async Task<ICloudTable<T>> GetTableAsync<T>() where T : TableData
        {
            await InitializeAsync();
            return new AzureCloudTable<T>(_client);
        }

        private async Task InitializeAsync()
        {
            if (_client.SyncContext.IsInitialized)
                return;

            var store = new MobileServiceSQLiteStore("offlinegeostat.db");

            store.DefineTable<Location>();
            store.DefineTable<GeoStatUser>();
            store.DefineTable<Group>();
            store.DefineTable<GroupUser>();

            await _client.SyncContext.InitializeAsync(store);
        }

        public async Task SyncOfflineCacheAsync()
        {
            await InitializeAsync();
           
            if (!await CrossConnectivity.Current
                                .IsRemoteReachable(_client.MobileAppUri.Host, 80))
            {
                Debug.WriteLine($"Cannot connect to {_client.MobileAppUri} right now - offline");
                return;
            }

            await _client.SyncContext.PushAsync();

            await PullTableAsync<Location>();
            await PullTableAsync<GroupUser>();
            await PullTableAsync<Group>();
            await PullTableAsync<GeoStatUser>();
        }

        private async Task PullTableAsync<T>() where T : TableData
        {
            var table = await GetTableAsync<T>();
            await table.PullAsync();
        }
    }
}
