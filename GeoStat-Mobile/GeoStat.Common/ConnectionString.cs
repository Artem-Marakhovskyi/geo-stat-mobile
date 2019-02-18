using System;
namespace GeoStat.Common
{
    public class ConnectionString
    {
#if ALPHA
        public static string BackendUri { get; } = "http://geostat-alpha.azurewebsites.net";
#elif BETA
        public static string BackendUri { get; } = "http://geostat-app.azurewebsites.net";
#endif
    }
}
