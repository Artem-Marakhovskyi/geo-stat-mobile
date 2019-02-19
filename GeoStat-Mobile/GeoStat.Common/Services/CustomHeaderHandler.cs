using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GeoStat.Common.Services
{
    public class CustomHeaderHandler : DelegatingHandler
    {
        private readonly UserContext _userContext;

        public CustomHeaderHandler(UserContext userContext)
        {
            _userContext = userContext;
        }
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            request.Headers.Add("GEOSTAT_AUTH", _userContext.Token);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
