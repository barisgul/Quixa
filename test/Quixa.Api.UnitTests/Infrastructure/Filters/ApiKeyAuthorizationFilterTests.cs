using System.Collections.Generic;
using FluentAssertions;
using Quixa.Api.Infrastructure.Configurations;
using Quixa.Api.Infrastructure.Filters;
using Quixa.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using Moq;
using Xunit;

namespace Quixa.Api.UnitTests.Infrastructure.Filters
{
    public class ApiKeyAuthorizationFilterTests
    {
        private const string _key = "test_secret_key";

        private bool _headersAccessed = false;
        private readonly ApiKeyAuthorizationFilter _filter;

        private readonly Mock<IFeatureManager> _featureManagerMock;

        public ApiKeyAuthorizationFilterTests()
        {
            var options = Options.Create(new ApiKeySettings { SecretKey = _key });
            _featureManagerMock = new Mock<IFeatureManager>(MockBehavior.Strict);
            _featureManagerMock.Setup(x => x.IsEnabledAsync(FeatureFlags.ApiKey))
                .ReturnsAsync(true);

            _filter = new ApiKeyAuthorizationFilter(options, _featureManagerMock.Object);
        }

        [Fact]
        public void When_feature_is_disabled_Then_should_immediately_returns()
        {
            //given
            _featureManagerMock.Setup(x => x.IsEnabledAsync(FeatureFlags.ApiKey))
                .ReturnsAsync(false);

            //when
            var context = GetMockedContext(secretKey:null);
            _filter.OnAuthorization(context);

            //then
            context.Result.Should().BeNull();
            _headersAccessed.Should().BeFalse();

            _featureManagerMock.VerifyAll();
        }

        [Fact]
        public void When_Authorization_header_not_presented_Then_should_return_Unauthorized()
        {
            //when
            var context = GetMockedContext(secretKey: null);
            _filter.OnAuthorization(context);

            //then
            context.Result.Should().BeOfType<UnauthorizedObjectResult>()
                .Which.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
            _headersAccessed.Should().BeTrue();

            _featureManagerMock.VerifyAll();
        }

        [Fact]
        public void When_Authorization_header_is_empty_Then_should_return_Unauthorized()
        {
            //when
            var context = GetMockedContext(secretKey: string.Empty);
            _filter.OnAuthorization(context);

            //then
            context.Result.Should().BeOfType<UnauthorizedObjectResult>()
                .Which.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
            _headersAccessed.Should().BeTrue();
        }

        [Fact]
        public void When_Authorization_header_has_invalid_format_Then_should_return_Unauthorized()
        {
            //when
            var context = GetMockedContext(secretKey: $"Key {_key}");
            _filter.OnAuthorization(context);

            //then
            context.Result.Should().BeOfType<UnauthorizedObjectResult>()
                .Which.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
            _headersAccessed.Should().BeTrue();
        }

        [Fact]
        public void When_Authorization_header_do_not_match_Then_should_return_Unauthorized()
        {
            //when
            var context = GetMockedContext(secretKey: "APIKey ABC");
            _filter.OnAuthorization(context);

            //then
            context.Result.Should().BeOfType<UnauthorizedObjectResult>()
                .Which.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
            _headersAccessed.Should().BeTrue();
        }

        [Fact]
        public void When_Authorization_header_match_Then_result_should_be_null()
        {
            //when
            var context = GetMockedContext(secretKey: $"APIKey {_key}");
            _filter.OnAuthorization(context);

            //then
            context.Result.Should().BeNull();
            _headersAccessed.Should().BeTrue();
        }

        private AuthorizationFilterContext GetMockedContext(string secretKey)
        {
            var headers = new HeaderDictionary();
            if (secretKey != null)
            {
                headers.Add("Authorization", new[] { secretKey });
            }

            var httpRequestMock = new Mock<HttpRequest>();
            httpRequestMock.Setup(mock => mock.Headers)
                .Returns(headers)
                .Callback(() => _headersAccessed = true);

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(m => m.Request)
                .Returns(httpRequestMock.Object);

            var actionContext = new ActionContext(
                httpContextMock.Object,
                new Mock<RouteData>().Object,
                new Mock<ActionDescriptor>().Object
                );

            return new AuthorizationFilterContext(actionContext, new List<IFilterMetadata>(0));
        }
    }
}
