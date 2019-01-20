using System;
using System.Threading.Tasks;

namespace GeoStat.Common.Abstractions
{
    public interface ICloudService
    {
        Task<ICloudTable<T>> GetTableAsync<T>() where T : TableData;
        Task SyncOfflineCacheAsync();
    }
}
