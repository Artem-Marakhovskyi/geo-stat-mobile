using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeoStat.Common.Abstractions;

namespace GeoStat.Common.Services
{
    public class GeoStatRepository<T> : IGeoStatRepository<T> where T : TableData
    {
        private readonly ICloudService _cloudService;
        private ICloudTable<T> _cloudTable;
        private Task _initializeTask;

        public GeoStatRepository(ICloudService cloudService)
        {
            _cloudService = cloudService;
        }

        private Task InitializeCloudTable()
        {
            if (_initializeTask == null)
                _initializeTask = GetInitializedTable();

            return _initializeTask;
        }

        private async Task GetInitializedTable()
        {
            _cloudTable = await _cloudService.GetTableAsync<T>();

            if (_cloudTable == null)
                throw new NullReferenceException();
        }

        public async Task DeleteItemAsync(T item)
        {
            await InitializeCloudTable();

            if (item.Id == null) throw new ArgumentNullException();

            await _cloudTable.DeleteItemAsync(item);
        }

        public async Task<ICollection<T>> ReadAllItemsAsync()
        {
            await InitializeCloudTable();

            return await _cloudTable.ReadAllItemsAsync();
        }

        public async Task<T> ReadItemByIdAsync(string id)
        {
            await InitializeCloudTable();

            return await _cloudTable.ReadItemAsync(id);
        }

        public async Task<ICollection<T>> ReadItemsAsync(int start, int count)
        {
            await InitializeCloudTable();

            return await _cloudTable.ReadItemsAsync(start, count);
        }

        public async Task<T> UpsertItemAsync(T item)
        {
            await InitializeCloudTable();

            if (item.Id == null)
                return await _cloudTable.CreateItemAsync(item);

            return await _cloudTable.UpdateItemAsync(item);
        }

        public async Task PullAsync()
        {
            await InitializeCloudTable();

            await _cloudTable.PullAsync();
        }
    }
}
