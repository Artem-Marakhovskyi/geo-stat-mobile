using System;
namespace GeoStat.Common.Abstractions
{
    public interface ISecureStorageService
    {
        void StoreData(string key, string value);
        string GetValueByKey(string key);
        void DeleteValueByKey(string key);
    }
}
