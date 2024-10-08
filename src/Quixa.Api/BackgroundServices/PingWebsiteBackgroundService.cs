using System.Net;
using System.Net.Http;
using Quixa.Core.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Quixa.Api.BackgroundServices
{
    public interface IPingService
    {
        public HttpStatusCode WebsiteStatusCode { get; }
    }

    public class PingWebsiteBackgroundService : BackgroundService, IPingService
    {
        private readonly PeriodicTimer _timer;
        private readonly HttpClient _client;
        private readonly ILogger<PingWebsiteBackgroundService> _logger;
        private readonly IOptions<PingWebsiteSettings> _configuration;

        public HttpStatusCode WebsiteStatusCode { get; private set; }

        public PingWebsiteBackgroundService(
            IHttpClientFactory httpClientFactory,
            ILogger<PingWebsiteBackgroundService> logger,
            IOptions<PingWebsiteSettings> configuration)
        {
            _client = httpClientFactory.CreateClient(nameof(PingWebsiteBackgroundService));
            _logger = logger;
            _configuration = configuration;

             _timer = new PeriodicTimer(TimeSpan.FromMinutes(_configuration.Value.TimeIntervalInMinutes));
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("{BackgroundService} running at '{Date}', pinging '{URL}'",
                    nameof(PingWebsiteBackgroundService), DateTime.Now, _configuration.Value.Url);
                try
                {
                    using var response = await _client.GetAsync(_configuration.Value.Url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                    WebsiteStatusCode = response.StatusCode;
                    _logger.LogInformation("Is '{Host}' responding: {Status}",
                        _configuration.Value.Url.Authority, response.IsSuccessStatusCode);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Error during ping");
                }
                await _timer.WaitForNextTickAsync(cancellationToken);
            }
            _timer.Dispose();
        }
    }
}
