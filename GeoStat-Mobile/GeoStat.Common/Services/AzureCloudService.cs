﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GeoStat.Common.Abstractions;
using GeoStat.Common.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Plugin.Connectivity;

namespace GeoStat.Common.Services
{
    public class AzureCloudService : ICloudService
    {
        private readonly MobileServiceClient _client;
        private readonly string _backendUri = "http://geostat-app.azurewebsites.net";

        public AzureCloudService()
        {
            _client = new MobileServiceClient(_backendUri);
        }

        public async Task<ICloudTable<T>> GetTableAsync<T>() where T : TableData
        {
            await InitializeAsync();
            return new AzureCloudTable<T>(_client);
        }

        async Task InitializeAsync()
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
                                .IsRemoteReachable(_client.MobileAppUri.Host, 443))
            {
                Debug.WriteLine($"Cannot connect to {_client.MobileAppUri} right now - offline");
                return;
            }

            await _client.SyncContext.PushAsync();

            await PullTable<Location>();
            await PullTable<GroupUser>();
            await PullTable<Group>();
            await PullTable<GeoStatUser>();
        }

        private async Task PullTable<T>() where T : TableData
        {
            var table = await GetTableAsync<T>();
            await table.PullAsync();
        }
    }
}
