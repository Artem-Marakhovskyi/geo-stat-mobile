using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeoStat.Common.Abstractions;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace GeoStat.Common.Services
{
    public class AzureCloudTable<T> : ICloudTable<T> where T : TableData
    {
        private readonly IMobileServiceSyncTable<T> _table;

        public AzureCloudTable(MobileServiceClient client)
        {
            _table = client.GetSyncTable<T>();
        }

        public async Task<T> CreateItemAsync(T item)
        {
            await _table.InsertAsync(item);
            return item;
        }

        public Task DeleteItemAsync(T item)
        {
            return _table.DeleteAsync(item);
        }

        public Task<List<T>> ReadAllItemsAsync()
        {
            return _table.ToListAsync();
        }

        public Task<T> ReadItemAsync(string id)
        {
            return _table.LookupAsync(id);
        }

        public Task<List<T>> ReadItemsAsync(int start, int count)
        {
            return _table.Skip(start).Take(count).ToListAsync();
        }

        public async Task<T> UpdateItemAsync(T item)
        {
            await _table.UpdateAsync(item);
            return item;
        }

        public Task PullAsync()
        {
            var queryName = $"incsync_{typeof(T).Name}";
            return _table.PullAsync(queryName, _table.CreateQuery());
        }

        public IMobileServiceTableQuery<T> CreateQuery()
        {
            return _table.CreateQuery();
        }
    }
}
