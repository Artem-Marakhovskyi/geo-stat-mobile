using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Newtonsoft.Json.Linq;

namespace GeoStat.Common.Services
{
    [Preserve(AllMembers = true)]
    public class MobileServiceSQLiteStoreWithLogging : MobileServiceSQLiteStore
    {
        private readonly bool _logResults;
        private readonly bool _logParameters;
        private readonly string _moduleName;

        public MobileServiceSQLiteStoreWithLogging(string fileName, string moduleName, bool logResults = false, bool logParameters = false)
            : base(fileName)
        {
            _moduleName = moduleName;
            _logResults = logResults;
            _logParameters = logParameters;
        }

        protected override IList<JObject> ExecuteQueryInternal(string tableName, string sql, IDictionary<string, object> parameters)
        {
#if DEBUG
            Console.WriteLine($"{_moduleName}|| {sql}");

            if (_logParameters)
            {
                PrintDictionary(parameters);
            }
#endif

            var result = base.ExecuteQueryInternal(tableName, sql, parameters);
#if DEBUG
            if (_logResults && result != null)
            {
                foreach (var token in result)
                {

                    Console.WriteLine($"{_moduleName}|| {token}");

                }
            }
#endif
            return result;
        }

        protected override void ExecuteNonQueryInternal(string sql, IDictionary<string, object> parameters)
        {
#if DEBUG
            Console.WriteLine($"{_moduleName}|| {sql}");

            if (_logParameters)
            {
                PrintDictionary(parameters);
            }
#endif

            base.ExecuteNonQueryInternal(sql, parameters);
        }

        private void PrintDictionary(IDictionary<string, object> dictionary)
        {
            if (dictionary == null)
            {
                return;
            }

            foreach (var pair in dictionary)
            {
                Console.WriteLine($"{_moduleName}|| {pair.Key}:{pair.Value}");
            }
        }
    }
}
