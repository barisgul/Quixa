using System.Net;
using Quixa.Api.BackgroundServices;

namespace Quixa.Api.IntegrationTests.Infrastructure.Fakes
{
    public class FakePingService : IPingService
    {
        internal const HttpStatusCode Result = HttpStatusCode.EarlyHints;

        public HttpStatusCode WebsiteStatusCode => Result;
    }
}
