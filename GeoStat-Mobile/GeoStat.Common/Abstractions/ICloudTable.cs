using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace GeoStat.Common.Abstractions
{
    public interface ICloudTable<T> where T : TableData
    {
        Task<T> CreateItemAsync(T item);
        Task<T> ReadItemAsync(string id);
        Task<T> UpdateItemAsync(T item);
        Task DeleteItemAsync(T item);
        Task<List<T>> ReadAllItemsAsync();
        Task<List<T>> ReadItemsAsync(int start, int count);
        Task PullAsync();
        IMobileServiceTableQuery<T> CreateQuery();
    }
}
