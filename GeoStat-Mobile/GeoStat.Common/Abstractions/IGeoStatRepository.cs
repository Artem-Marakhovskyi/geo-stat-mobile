using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoStat.Common.Abstractions
{
    public interface IGeoStatRepository<T> where T : TableData
    {
        Task<T> UpsertItemAsync(T item);
        Task DeleteItemAsync(T item);
        Task<T> ReadItemByIdAsync(string id);
        Task<ICollection<T>> ReadAllItemsAsync();
        Task<ICollection<T>> ReadItemsAsync(int start, int count);
        Task PullAsync();
    }
}
