using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Logging;

namespace GeoStat.Common.Services
{
    public class LoggingHandler : DelegatingHandler
    {
        private readonly IMvxLog _logger;

        public LoggingHandler(IMvxLog log)
        {
            _logger = log;
        }

        protected async override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            _logger.Info("Start of request");
            var response = await base.SendAsync(request, cancellationToken);
            _logger.Info("End of request");

            return response;
        }
    }
}
